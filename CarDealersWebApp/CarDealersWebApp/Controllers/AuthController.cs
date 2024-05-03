using CarDealersWebApp.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Data.Repositories;
using CarDealersWebApp.Models.Entities;

namespace CarDealersWebApp.Controllers;

public class AuthController : Controller
{
    private UserRepository userDatabase = new UserRepository();

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegistrationViewModel viewModel)
    {
        var user = new User
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            Password = viewModel.Password,
            Phone = viewModel.Phone,
            Country = viewModel.Country,
            Address = viewModel.Address
        };

        userDatabase.SaveUser(user);

        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
}
