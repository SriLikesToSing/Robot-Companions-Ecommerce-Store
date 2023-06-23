using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace robotCompanions.Models.Repositories
{
    public class userOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public userOrderRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, 
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Order>> userOrders()
        {
            var userId = getUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged in");
            var orders = await _db.order.Include(x=>x.orderStatus).Include(x=>x.orderDetails)
                .ThenInclude(x=>x.robot).ThenInclude(x=>x.robotSize).Where(a => a.userId == userId).ToListAsync();
            Debug.WriteLine(orders);
            return orders;
        }
        private string getUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

    }
}
