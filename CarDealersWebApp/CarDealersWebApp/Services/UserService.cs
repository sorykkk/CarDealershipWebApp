using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Models.Auth;

namespace CarDealersWebApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    public UserService(IUserRepository userRepository) =>
        this.userRepository = userRepository;

    public async Task CreateUserAsync(RegistrationViewModel registerViewModel)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerViewModel.Password);
        var user = new User
        {
            Name = registerViewModel.Name,
            Email = registerViewModel.Email,
            Password = hashedPassword,
            Phone = registerViewModel.Phone,
            Country = registerViewModel.Country ?? string.Empty,
            Address = registerViewModel.Address ?? string.Empty,
        };

        await userRepository.SaveUser(user);
    }

    public Task<User> GetUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(string email)
    {
        throw new NotImplementedException();
    }
}
