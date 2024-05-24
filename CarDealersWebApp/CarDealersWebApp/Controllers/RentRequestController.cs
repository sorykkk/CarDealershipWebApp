using Microsoft.AspNetCore.Mvc;

namespace CarDealersWebApp.Controllers;

public class RentRequestController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
