using CarDealersWebApp.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Data.Repositories;
using CarDealersWebApp.Services;
using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Controllers;

public class AuthController : Controller
{
    private readonly IUserService userService;

    public AuthController(IUserService userService)
    {
        this.userService = userService;
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await userService.CreateUserAsync(viewModel);

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel viewModel) {

        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }

        return RedirectToAction("Index", "Home");
    }
}
