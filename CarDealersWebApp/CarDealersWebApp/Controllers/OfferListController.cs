
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Services;
using CarDealersWebApp.Models.Dealer;
using CarDealersWebApp.Data.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CarDealersWebApp.Controllers;

//[Authorize(Policy = "DealerOnly")]
public class OfferListController : Controller
{
    private readonly ICarService carService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public OfferListController(IWebHostEnvironment webHostEnvironment,ICarService carService)
    {
        this.carService = carService;
        _webHostEnvironment = webHostEnvironment;
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
            // Log the validation errors
            /*var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                // Log the error message
                Console.WriteLine(error.ErrorMessage);
            }*/

            await carService.GetCarsAsync(viewModel.ExistingCars, userEmail);
            return View(viewModel);
        }

        string wwwRootPath = _webHostEnvironment.WebRootPath;

        if(viewModel.NewCarViewModel.file != null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.NewCarViewModel.file.FileName);
            string productPath = Path.Combine(wwwRootPath, @"Images\Car");

            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
            {
                viewModel.NewCarViewModel.file.CopyTo(fileStream);
            }

            viewModel.NewCarViewModel.ImagePath = @"\Images\Car\" + fileName;
        }
        else
        {
            viewModel.NewCarViewModel.ImagePath = @"\Images\Car\car_unknown.jpg";
        }
        await carService.CreateCarAsync(viewModel.NewCarViewModel, userEmail);
        TempData["success"] = "Car added successfully to your offer list";

        //ModelState.Clear();
        viewModel.NewCarViewModel = new NewCarViewModel();
        await carService.GetCarsAsync(viewModel.ExistingCars, userEmail);
        return RedirectToAction("OfferList");
    }

    /*[HttpPost]
    public async Task<IActionResult> DisplayCars(CarListViewModel viewModel)
    {
        var userEmail = HttpContext.Session.GetString("Email") ?? string.Empty;
        //add here the all new cars
        await carService.GetCarsAsync(viewModel, userEmail);
        return Redirect("OfferList");
    }*/

}
