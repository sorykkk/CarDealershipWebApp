using CarDealersWebApp.Data.Entities;
using System.Data.SQLite;

namespace CarDealersWebApp.Data.Interfaces
{
    public interface IDealerRepository
    {
        Dealer GetDealer(int id);
        void SaveDealer(Dealer dealer);
    }
}
