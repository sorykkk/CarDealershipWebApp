using CarDealersWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using CarDealersWebApp.Models.Entities;
using CarDealersWebApp.Data;

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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            /*Dealer dealer = new Dealer();
            dealer.Name = "Toyota";
            dealer.Address = "Unirii str 20/2";
            dealer.Country = "Moldova";
            dealer.IsSelling = true;
            dealer.Cars = null;

            dealerRepo.SaveDealer(dealer);*/
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
