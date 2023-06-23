using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace robotCompanions.Controllers
{
    [Authorize]
    public class userOrder : Controller
    {
        private readonly IUserOrderRepository _userOrderRepo;
        public userOrder(IUserOrderRepository userOrderRepo)
        {
            _userOrderRepo = userOrderRepo;
        } 
        public async Task<IActionResult> userOrders()
        {
            var orders = await _userOrderRepo.userOrders();
            return View(orders);  
        }
    }
}
