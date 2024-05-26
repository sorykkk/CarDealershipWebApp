using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Models.Dealer;
using CarDealersWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarDealersWebApp.Controllers;

public class EditCarController : Controller
{
    private readonly ICarService carService;
    public EditCarController(ICarService carService)
    {
        this.carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> EditCar(int id)
    {
        Car? car = await carService.GetCarByIdAsync(id);
        if(car is null)
        {
            TempData["faile"] = "No such car was found";
            return RedirectToAction("OfferList", "OfferList");
        }

        EditCarViewModel viewModel = new EditCarViewModel
        {
            Id = car.Id,
            BrandName = car.BrandName,
            Model = car.Model,
            Description = car.Description,
            FuelType = car.FuelType,
            HP = car.HP,
            ImagePath = car.ImagePath,
            Mileage = car.Mileage,
            Price = car.Price,
            Year = car.Year,
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditCar(EditCarViewModel viewModel)
    {

        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if(viewModel.file is not null)
        {
            viewModel.ImagePath = carService.UploadNewImage(viewModel.file);
        }

        await carService.UpdateCarIdAsync(viewModel);

        TempData["success"] = "Car from your offer list edited successfully";
        return RedirectToAction("OfferList", "OfferList");
    }
}
