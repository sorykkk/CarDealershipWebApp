using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Models.Auth;
using System.Security.Cryptography;
using System.Text;

namespace CarDealersWebApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    public UserService(IUserRepository userRepository) =>
        this.userRepository = userRepository;

    public async Task CreateUserAsync(RegistrationViewModel registerViewModel)
    {
        string hashedPassword;
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(registerViewModel.Password));
        hashedPassword = BitConverter.ToString(bytes).Replace("-", "").ToLower();
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

    public async Task<User> GetUserAsync(string email)
    {
        User? user = await userRepository.GetUserByEmail(email);

        return user;
    }
}
