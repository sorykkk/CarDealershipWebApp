using CarDealersWebApp.Models.Dealer;
using Microsoft.AspNetCore.Mvc;

namespace CarDealersWebApp.Controllers;

public class RentRequestController : Controller
{
    [HttpGet]
    public IActionResult RentRequest()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RentRequest(RentRequestViewModel viewModel)
    {
        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }

        return View();
    }
}
