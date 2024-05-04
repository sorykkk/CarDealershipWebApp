using CarDealersWebApp.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Data.Repositories;
using CarDealersWebApp.Services;

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
        string email = viewModel.Email;
        string password = viewModel.Password;

        //aici treb sa pun logica cand nu este gasit userul
        //bool found =  userDatabase.ExistUser(email, password);

        return View();
    }
}
