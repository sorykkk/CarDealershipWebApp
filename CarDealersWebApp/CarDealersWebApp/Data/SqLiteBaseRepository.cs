using System.Data.SQLite;

namespace CarDealersWebApp.Data
{
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
