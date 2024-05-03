using CarDealersWebApp.Models.Entities;
using System.Data.SQLite;

namespace CarDealersWebApp.Data
{
    public interface IDealerRepository
    {
        Dealer GetDealer(int id);
        void SaveDealer(Dealer dealer);
    }

    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\LocalDataBase.sqlite"; }
        }

        public static SQLiteConnection DbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
}
