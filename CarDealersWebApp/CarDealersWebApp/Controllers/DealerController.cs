using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Controllers
{
    public class DealerController : Controller
    {
        private readonly IDealerRepository _dealerRepository;
        public DealerController (IDealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }
        [HttpGet]
        public IActionResult AllDealers()
        {
            Dealer dealer = _dealerRepository.GetDealer(1);

            return View(dealer);
        }
    }
}
