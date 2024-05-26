using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Models.Dealer;

namespace CarDealersWebApp.Services
{
    public interface ICarService
    {
        Task CreateCarAsync(NewCarViewModel viewModel, string userEmail);
        Task GetCarsAsync(List<ExistingCarViewModel> viewModels, string userEmail);
        Task<bool> DeleteCarByIdAsync(int carId);
        Task<bool> UpdateCarIdAsync(EditCarViewModel viewModel);
        Task<Car?> GetCarByIdAsync(int carId);
        string UploadNewImage(IFormFile file);
    }
}
