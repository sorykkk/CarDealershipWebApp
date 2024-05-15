using CarDealersWebApp.Models.Auth;
using CarDealersWebApp.Models.Dealer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CarDealersWebApp.Controllers;

//[Authorize(Policy = "DealerOnly")]
public class OfferListController : Controller
{
    [HttpGet]
    public async Task<IActionResult> OfferList(OfferListViewModel viewModel)
    {
        return View(viewModel);
    }


}
