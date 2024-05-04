using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using Dapper;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Data.Entity;

namespace CarDealersWebApp.Data.Repositories
{
    public class DealerRepository : SqLiteBaseRepository, IDealerRepository
    {
        private static string _tableName = "DealerProfile";
        public void SaveDealer(Dealer dealer)
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            using (var cnn = DbConnection())
            {
                cnn.Open();
                dealer.Id = cnn.Query<int>(
                    $@"INSERT INTO {_tableName}
                    ( Name, Address, Country, IsSelling, Cars ) VALUES
                    (@Name, @Address, @Country, @IsSelling, @Cars);
                    select last_insert_rowid()", dealer).First();
            }
        }

        public Dealer DeleteDealer(int Id)
        {
            using (var cnn = DbConnection())
            {
                cnn.Open();
                Dealer deleted = cnn.Query<Dealer>(
                    $@"DELETE FROM {_tableName} WHERE ID = @Id", new { Id }).FirstOrDefault();

                return deleted;
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = DbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    $@"create table {_tableName}
                    (
                        ID         INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name       varchar(100) not null,
                        Password   varchar(50)  not null,
                        Address    varchar(100) not null,
                        Country    varchar(100) not null,
                        IsSelling  integer      not null,
                        Cars       integer
                    )"
                    );
            }

        }
        public Dealer GetDealer(int id)
        {
            if (!File.Exists(DbFile))
            {
                return null;
            }
            using (var cnn = DbConnection())
            {
                cnn.Open();
                Dealer result = cnn.Query<Dealer>(
                    $@"SELECT Id, Name, Address, Country, IsSelling, Cars
                        FROM {_tableName}
                            WHERE id = @id", new { id }).FirstOrDefault();

                return result;
            }
        }
    }

}
