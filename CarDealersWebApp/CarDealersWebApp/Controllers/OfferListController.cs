
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Services;
using CarDealersWebApp.Models.Dealer;

namespace CarDealersWebApp.Controllers;

//[Authorize(Policy = "DealerOnly")]
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

        /*string wwwRootPath = _webHostEnvironment.WebRootPath;

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
        }*/

        
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
