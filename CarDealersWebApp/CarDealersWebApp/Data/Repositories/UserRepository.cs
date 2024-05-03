
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Models.Entities;
using Dapper;

namespace CarDealersWebApp.Data.Repositories
{
    public class UserRepository : SqLiteBaseRepository, IUserRepository
    {
        private static string _tableName = "UserProfile";
        public void SaveUser(User user)
        {
            user.Password = HashPassword(user.Password);

            if (!File.Exists(DbFile))
                CreateUserDatabase();

            using (var cnn = DbConnection())
            {
                cnn.Open();
                user.Id = cnn.Query<int>(
                    $@"INSERT INTO {_tableName}
                    ( Name, Phone, Password, Email, Address, Country ) VALUES
                    (@Name, @Phone, @Password, @Email, @Address, @Country);
                    select last_insert_rowid()", user).First();
            }
        }

        public bool ExistUser(string name, string password)
        {
            using (var cnn = DbConnection())
            {
                cnn.Open();

                string hashedPassword = HashPassword(password);
                User? findUser = cnn.Query<User>(
                    $@"SELECT Name, Password 
                        FROM {_tableName} 
                            WHERE Name = @name and Password = @password", new {name, hashedPassword}
                    ).FirstOrDefault();


                return findUser != null;
            }
        }

        private string HashPassword(string password )
        {
            return BCrypt.Net.BCrypt.HashPassword( password ); 
        }

        public User? DeleteUser(int Id)
        {
            using (var cnn = DbConnection())
            {
                cnn.Open();
                User? deleted = cnn.Query<User>(
                    $@"DELETE FROM {_tableName} WHERE ID = @Id", new { Id }).FirstOrDefault();

                return deleted;
            }
        }

        private static void CreateUserDatabase()
        {
            using (var cnn = DbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    $@"create table {_tableName}
                    (
                        ID         INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name       varchar(100) not null,
                        Phone      varchar(20),
                        Password   varchar(255)  not null,
                        Email      varchar(100) not null,
                        Address    varchar(100) not null,
                        Country    varchar(100) not null
                    )"
                    );
            }

        }
        public User? GetUser(int Id)
        {
            if (!File.Exists(DbFile))
                return null;

            using (var cnn = DbConnection())
            {
                cnn.Open();
                User? result = cnn.Query<User>(
                    $@"SELECT Id, Name, Phone, Password, Email, Address, Country
                        FROM {_tableName}
                            WHERE id = @Id", new { Id }).FirstOrDefault();

                return result;
            }
        }
    }
}
