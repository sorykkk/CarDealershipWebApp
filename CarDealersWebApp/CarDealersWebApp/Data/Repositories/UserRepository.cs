
using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using Dapper;
using System.Data.SQLite;
using System.Security.Cryptography.Xml;

namespace CarDealersWebApp.Data.Repositories
{
    public class UserRepository : SqLiteBaseRepository, IUserRepository
    {
        public UserRepository()
        {
            if (!File.Exists(DbFile))
                CreateUserTable();
        }

        public async Task<int> SaveUser(User user)
        {
            using var cnn = DbConnection();
            cnn.Open();
            CreateUserTable();
            var userId = (await cnn.QueryAsync<int>(
                $@"INSERT INTO Users
                ( Name, UserType, Phone, Password, Email, Address, Country ) VALUES
                (@Name, @Type, @Phone, @Password, @Email, @Address, @Country);
                select last_insert_rowid()", user)).First();
            return userId;
        }

        public async Task <User?> GetUserByEmail(string email)
        {
            using var cnn = DbConnection();
            cnn.Open();

            User? dbUser = ( await cnn.QueryAsync<User>(
                $@"SELECT *, UserType AS Type
                    FROM Users
                        WHERE Email = @email", new {email}
                )).FirstOrDefault();

            return dbUser;
        }

        public async Task <User?> DeleteUser(int Id)
        {
            using var cnn = DbConnection() ;
                cnn.Open();
                User? deleted = (await cnn.QueryAsync<User>(
                    $@"DELETE FROM Users WHERE ID = @Id", new { Id })).FirstOrDefault();

                return deleted;
        }

        private static void CreateUserTable()
        {
            using var cnn = DbConnection();
            cnn.Open();

            cnn.Execute(
                $@"create table IF NOT EXISTS Users 
                (
                    ID        INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserType      INTEGER not null,
                    Name          varchar(100) not null,
                    Phone         varchar(20),
                    Password      varchar(255) not null,
                    Email         varchar(100) not null,
                    Address       varchar(100),
                    Country       varchar(100)
                );

                INSERT INTO Users (UserType, Name, Phone, Password, Email, Address, Country) 
                VALUES
                    (1, 'd', 'asdfasdf', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'd@gmail.com', 'asdfsadf', 'asdf'),
                    (1, 'dd', '123455', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'dd@gmail.com', 'sdfasdf', 'asdfasdf');
                "
                );
        }

        public async Task <User?> GetUser(int id)
        {
            using var cnn = DbConnection();
            cnn.Open();
            User? result = (await cnn.QueryAsync<User>(
                $@"SELECT *
                    FROM Users
                        WHERE id = @id", new { id })).FirstOrDefault();

            return result;
        }
    }
}
