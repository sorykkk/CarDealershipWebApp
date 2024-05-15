using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Models.Auth;

namespace CarDealersWebApp.Services;

public interface IUserService
{
    Task CreateUserAsync(RegistrationViewModel registerViewModel);
    Task<User?> GetUserAsync(int userId);
    Task<User?> GetUserAsync(string email);
    Task<User> LoginUserAsync(string email, string password);
}
