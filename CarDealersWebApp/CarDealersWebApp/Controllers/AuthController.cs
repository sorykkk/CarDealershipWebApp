using CarDealersWebApp.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Services;
using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Exceptions;
using CarDealersWebApp.Models;
using CarDealersWebApp.Data.Repositories;

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
        
        User? loggedUser = null;
        try
        {
            loggedUser = await userService.LoginUserAsync(viewModel.Email, viewModel.Password);
        }
        catch(LoginException ex)
        {
            ModelState.AddModelError(nameof(viewModel.Password), ex.Message);
            return View(viewModel);
        }

        if (loggedUser != null)
        {
            HttpContext.Session.SetString("Email", loggedUser.Email);
            HttpContext.Session.SetString("Name", loggedUser.Name);

            string Type = "Customer";
            if (loggedUser.Type == UserType.Dealer)
                Type = "Dealer";

            HttpContext.Session.SetString("Type", Type);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task <IActionResult> Logout()
    {
        HttpContext.Session.SetString("Type", "");
        HttpContext.Session.SetString("Email", "");
        HttpContext.Session.SetString("Name", "");

        return RedirectToAction("Index", "Home");  
    }   
}
