using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Data.Interfaces
{
    public interface IUserRepository
    {
        Task <int> SaveUser(User user);
        Task <User?> GetUser(int id);
        Task <User?> GetUserByEmail(string email);
        Task <User?> DeleteUser(int id);
        Task DeleteTable();
    }
}
