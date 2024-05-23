using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Data.Interfaces
{
    public interface IUserRepository
    {
        static string UserTableName { get; set; } = "UserProfile";
        static string UserTableId { get; set; } = "ID";
        Task <int> SaveUser(User user);
        Task <User?> GetUser(int id);
        Task <User?> GetUserByEmail(string email);
        Task <User?> DeleteUser(int id);
    }
}
