using CarDealersWebApp.Models.Entities;

namespace CarDealersWebApp.Data.Interfaces
{
    public interface IUserRepository
    {
        void SaveUser(User user);
        User? GetUser(int Id);
        User? DeleteUser(int Id);
    }
}
