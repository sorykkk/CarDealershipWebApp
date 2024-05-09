using CarDealersWebApp.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Data.Repositories;
using CarDealersWebApp.Services;
using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Exceptions;

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

        try
        {
            await userService.CreateUserAsync(viewModel);
        }
        catch (UserExists ex) {
            ModelState.AddModelError(nameof(viewModel.Email), ex.Message);
            return View(viewModel);
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task <IActionResult> Login(LoginViewModel viewModel) {

        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }

        User loggedUser = await userService.GetUserAsync(viewModel.Email);
        if (loggedUser == null)
        {
            return View(viewModel);
        }

        /*viewModel.Name = loggedUser.Name;
        viewModel.IsLogged = true;*/
        HttpContext.Session.SetString("Email", loggedUser.Email);
        HttpContext.Session.SetString("Name", loggedUser.Name);

        return RedirectToAction("Index", "Home");
    }
}
