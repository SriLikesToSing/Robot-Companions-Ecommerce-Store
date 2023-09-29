using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Humanizer;
using Microsoft.Build.Framework;
using Microsoft.Build.Execution;
using Microsoft.AspNetCore.Mvc;

namespace robotCompanions.Models.Repositories
{
    public class cartRepositories : ICartRepository 
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public cartRepositories(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> addItem(int robotId, int qty)
        {
            Debug.WriteLine(robotId);
            using var transaction = _db.Database.BeginTransaction();
            string userId = getUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user it not logged in.");
                var cart = await getCart(userId);
                if (cart is null)
                {
                    cart = new shoppingCart
                    {
                        userId = userId
                    };
                    _db.shoppingCart.Add(cart);
                }
                _db.SaveChanges();
                var cartItem = _db.cartDetails.FirstOrDefault(a => a.shoppingCartId == cart.Id && a.robotId == robotId);
                if (cartItem is not null)
                {
                    cartItem.quantity += qty;
                }
                else
                {
                    var Robots = _db.Robots.Find(robotId);
                    cartItem = new cartDetails
                    {
                        shoppingCartId = cart.Id,
                        robotId = robotId,
                        quantity = qty,
                        RobotsId = robotId,
                        unitPrice = Robots.price
                    };
                   _db.cartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await getCartItemCount(userId);
            return cartItemCount;
        }
        public async Task<int> removeItem(int robotId)
        {
            //            using var transaction = _db.Database.BeginTransaction();
            string userId = getUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user not logged in.");
                var cart = await getCart(userId);
                if (cart is null)
                {
                }
                _db.SaveChanges();
                cartDetails? cartItem = _db.cartDetails.FirstOrDefault(a => a.shoppingCartId == cart.Id && a.robotId == robotId);
                if (cartItem is null)
                {
                }
                else if (cartItem.quantity == 1)
                {
                    _db.cartDetails.Remove(cartItem);

                }
                else
                {
                    cartItem.quantity = cartItem.quantity - 1;
                }
                _db.SaveChanges();
                //transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await getCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<shoppingCart> getUserCart(){
            var userId = getUserId();
            if(userId == null)
                throw new Exception("Invalid User-ID");
            var shoppingCart = await _db.shoppingCart.Include(a => a.cartDetails).ThenInclude(a => a.Robots).ThenInclude(a => a.robotSize).Where(a => a.userId == userId).FirstOrDefaultAsync();

            return shoppingCart;
        }

        public async Task<string> doCheckout()
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var userId = getUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged in.");
                }
                var cart = await getCart(userId);
                if (cart is null)
                {
                    throw new Exception("invalid cart");
                }
                var cartDetails = _db.cartDetails.Where(a => a.shoppingCartId == cart.Id).ToList();
                if (cartDetails.Count == 0)
                {
                    throw new Exception("Cart is empty.");
                }

                var order = new Order
                {
                    userId = userId,
                    createDate = DateTime.UtcNow,
                    orderStatusId=3,
                };

                _db.order.Add(order);
                _db.SaveChanges();
                foreach(var item in cartDetails)
                {
                    var orderDetail = new orderDetails
                    {
                        robotId = item.robotId,
                        orderId = order.Id,
                        quantity = item.quantity,
                        unitPrice = item.unitPrice
                    };
                    _db.orderDetails.Add(orderDetail);
                }
                _db.SaveChanges();

                _db.cartDetails.RemoveRange(cartDetails);
                _db.SaveChanges();
                transaction.Commit();
                return "WORKED";

            }catch(Exception ex)
            {
                return ex.ToString(); 
            }
        }

        public async Task<shoppingCart> getCart(string userId)
        {
            var cart = await _db.shoppingCart.FirstOrDefaultAsync(x => x.userId == userId);
            return cart;
        } 

        public async Task<int> getCartItemCount(string userId="")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = getUserId();
            }

            var data = await (from cart in _db.shoppingCart
                              join cartDetail in _db.cartDetails
                              on cart.Id equals cartDetail.shoppingCartId
                              select new { cartDetail.Id }
                              ).ToListAsync();

            return data.Count-1;
        }

        private string getUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
