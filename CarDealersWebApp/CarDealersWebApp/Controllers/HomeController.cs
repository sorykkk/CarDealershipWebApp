using CarDealersWebApp.Data.Repositories;
using CarDealersWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CarDealersWebApp.Controllers
{

    public class HomeController : Controller
    {
        private DealerRepository dealerRepo = new DealerRepository();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /*[HttpPost]
        public IActionResult Register(User user)
        {
            using (var cnn = DbConnection())
            {
                try
                {
                    if(ModelState.IsValid)
                    {
                        User regList = User.Registration
                    }
                }
            }
        }*/

        public IActionResult Index()
        {
            return View();
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
