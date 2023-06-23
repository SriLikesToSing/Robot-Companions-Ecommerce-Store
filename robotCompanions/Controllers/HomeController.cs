using Microsoft.AspNetCore.Mvc;
using robotCompanions.Models;
using robotCompanions.Models.DTOs;
using System.Diagnostics;

namespace robotCompanions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository _homeRepository)
        {
            homeRepository = _homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sTerm="", int sizeId=0)
        {
            IEnumerable<Robots> robots = await homeRepository.getRobots(sTerm, sizeId);
            IEnumerable<robotSize> robotSizes = await homeRepository.robotSizes();
            robotDisplayModel robotModel = new robotDisplayModel
            {
                Robots = robots,
                robotSizes = robotSizes,
                sTerm = sTerm,
                robotSizeId = sizeId
            };
            return View(robotModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}