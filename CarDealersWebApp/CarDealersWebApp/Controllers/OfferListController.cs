
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Services;
using CarDealersWebApp.Models.Dealer;

namespace CarDealersWebApp.Controllers;

public class OfferListController : Controller
{
    private readonly ICarService carService;

    public OfferListController(ICarService carService)
    {
        this.carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> OfferList()
    {
        var viewModel = new CarListViewModel();
        var userEmail = HttpContext.Session.GetString("Email") ?? string.Empty;
        await carService.GetCarsAsync(viewModel.ExistingCars, userEmail);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> OfferList(CarListViewModel viewModel)
    {
        var userEmail = HttpContext.Session.GetString("Email") ?? string.Empty;
        if (!ModelState.IsValid)
        {
            await carService.GetCarsAsync(viewModel.ExistingCars, userEmail);
            return View(viewModel);
        }
        
        viewModel.NewCarViewModel.ImagePath = carService.UploadNewImage(viewModel.NewCarViewModel.file);

        await carService.CreateCarAsync(viewModel.NewCarViewModel, userEmail);
        TempData["success"] = "Car added successfully to your offer list";

        viewModel.NewCarViewModel = new NewCarViewModel();
        await carService.GetCarsAsync(viewModel.ExistingCars, userEmail);
        return RedirectToAction("OfferList");
    }

    public async Task<IActionResult> DeleteCar(int id)
    {
        bool success = await carService.DeleteCarByIdAsync(id);
        if (success)
            TempData["success"] = "Car deleted successfuly from your offer list";
        else TempData["fail"] = "Car couldn't be deleted";

        return RedirectToAction("OfferList");

    }

    public async Task<IActionResult> EditCar(int id)
    {
        return RedirectToAction("EditCar", "EditCar", new {id = id});
    }
}
