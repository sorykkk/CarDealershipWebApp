
using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using Dapper;

namespace CarDealersWebApp.Data.Repositories
{
    public class UserRepository : SqLiteBaseRepository, IUserRepository
    {
        private static string _tableName = "UserProfile";

        public UserRepository()
        {
            if (!File.Exists(DbFile))
                CreateUserTable();
        }

        public async Task<int> SaveUser(User user)
        {
            using var cnn = DbConnection();
            cnn.Open();
            var userId = cnn.Query<int>(
                $@"INSERT INTO {_tableName}
                ( Name, Phone, Password, Email, Address, Country ) VALUES
                (@Name, @Phone, @Password, @Email, @Address, @Country);
                select last_insert_rowid()", user).First();
            
            return userId;
        }

        public async Task <User?> GetUserByEmail(string email)
        {
            using var cnn = DbConnection();
            cnn.Open();

            User? dbUser = cnn.Query<User>(
                $@"SELECT * 
                    FROM {_tableName} 
                        WHERE Email = @email", new {email}
                ).FirstOrDefault();

            return dbUser;
        }

        public async Task <User?> DeleteUser(int Id)
        {
            using var cnn = DbConnection() ;
                cnn.Open();
                User? deleted = cnn.Query<User>(
                    $@"DELETE FROM {_tableName} WHERE ID = @Id", new { Id }).FirstOrDefault();

                return deleted;
        }

        private static void CreateUserTable()
        {
            using var cnn = DbConnection();
            cnn.Open();
            cnn.Execute(
                $@"create table {_tableName}
                (
                    ID         INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name       varchar(100) not null,
                    Phone      varchar(20),
                    Password   varchar(255) not null,
                    Email      varchar(100) not null,
                    Address    varchar(100),
                    Country    varchar(100)
                )"
                );
        }

        public async Task <User?> GetUser(int id)
        {
            using var cnn = DbConnection();
            cnn.Open();
            User? result = cnn.Query<User>(
                $@"SELECT *
                    FROM {_tableName}
                        WHERE id = @id", new { id }).FirstOrDefault();

            return result;
        }
    }
}
