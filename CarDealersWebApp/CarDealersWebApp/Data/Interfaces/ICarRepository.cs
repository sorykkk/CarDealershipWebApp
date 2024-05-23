using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Data.Interfaces;

public interface ICarRepository
{
    static string CarTableName { get; set; } = "CarProfile";
    static string CarTableId { get; set; } = "CAR_ID";
    Task<int> SaveCar(Car car, int dealerId);
    public Task<List<Car>> GetCarListOfUser(string userEmail);
    public Task DeleteTable();
    /*Task<Car?> GetCar(int id);
    Task<Car?> DeleteCar(int id);*/
}
