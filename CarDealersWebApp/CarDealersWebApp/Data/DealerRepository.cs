using CarDealersWebApp.Models.Entities;
using Dapper;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Data.Entity;

namespace CarDealersWebApp.Data
{
    public class DealerRepository : SqLiteBaseRepository
    {
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
                    @"INSERT INTO Dealer
                    ( Name, Address, Country, IsSelling, Cars ) VALUES
                    (@Name, @Address, @Country, @IsSelling, @Cars);
                    select last_insert_rowid()", dealer).First();
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = DbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table Dealer
                    (
                        ID         integer identity primary key AUTOINCREMENT,
                        Name       varchar(100) not null,
                        Address    varchar(100) not null,
                        Country    varchar(100) not null,
                        IsSelling  integer not null,
                        Cars       integer
                    )"
                    );
            }

        }
        public Dealer GetDealer(int id)
        {
            if (!File.Exists(DbFile)) 
                return null;
            using (var cnn = DbConnection())
            {
                cnn.Open();
                Dealer result = cnn.Query<Dealer>(
                    @"SELECT Id, Name, Address, Country, IsSelling, Cars
                        FROM Dealer
                            WHERE id = @id", new {id}).FirstOrDefault();
                return result;
            }
        }
    }

}
