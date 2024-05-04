using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Data.Interfaces
{
    public interface IUserRepository
    {
        int SaveUser(User user);
        User? GetUser(int id);
        User? GetUserByEmail(string email);
        User? DeleteUser(int id);
    }
}
