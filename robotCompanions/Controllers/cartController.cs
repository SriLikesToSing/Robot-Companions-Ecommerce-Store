using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace robotCompanions.Controllers
{
    [Authorize]
    public class cartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public cartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> addItem(int robotId, int qty = 1, int redirect=0)
        {
            Debug.WriteLine("WHATS UP BITCH ");
            var cartCount = await _cartRepo.addItem(robotId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("getUserCart");
        }

        public async Task<IActionResult> removeItem(int robotId)
        {
            var cartCount = await _cartRepo.removeItem(robotId);
            return RedirectToAction("getUserCart");
        }

        public async Task<IActionResult> getUserCart()
        {
            var cart = await _cartRepo.getUserCart();
            return View(cart);
        }

        public async Task<IActionResult> checkout()
        {
            bool isCheckedOut = await _cartRepo.doCheckout();
            if (!isCheckedOut)
                throw new Exception("Something happen in server side");
            return RedirectToAction("Index", "Home");
            

        }

        public async Task<IActionResult> getTotalItemInCart()
        {
            int cartItem = await _cartRepo.getCartItemCount();
            return Ok(cartItem);
        }
    }
}
