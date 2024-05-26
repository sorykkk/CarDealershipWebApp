using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Repositories;
using CarDealersWebApp.Exceptions;
using CarDealersWebApp.Models.Auth;
using System.Security.Claims;

namespace CarDealersWebApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task CreateUserAsync(RegistrationViewModel registerViewModel)
    {

        User? existingUser = await userRepository.GetUserByEmail(registerViewModel.Email);

        if(existingUser is not null)
        {
            throw new UserExists("This email already exists.");
        }

        string hashedPassword = PasswordHelper.HashPassword(registerViewModel.Password);
        var user = new User
        {
            Name = registerViewModel.Name,
            Email = registerViewModel.Email,
            Password = hashedPassword,
            Phone = registerViewModel.Phone,
            Type = registerViewModel.IsDealer? UserType.Dealer : UserType.Customer,
            Country = registerViewModel.Country ?? string.Empty,
            Address = registerViewModel.Address ?? string.Empty,
        };

        await userRepository.SaveUser(user);
    }

    public Task<User?> GetUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserAsync(string email)
    {
        User? user = await userRepository.GetUserByEmail(email);

        return user;
    }

    public async Task<User> LoginUserAsync(string email, string password)
    {
        User? existingUser = await userRepository.GetUserByEmail(email);
        string errorMessage = "Incorrect email or password";
        if (existingUser is null)
        {
            throw new LoginException(errorMessage);
        }

        var hashedPassword = PasswordHelper.HashPassword(password);
        if(!existingUser.Password.Equals(hashedPassword))
        {
            throw new LoginException(errorMessage);
        }


        var claims = new List<Claim>();
        if(existingUser.Type == UserType.Dealer)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Dealer"));
        }
        else if(existingUser.Type == UserType.Customer)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Customer"));
        }

        return existingUser;
    }
}
