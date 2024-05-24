using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Models.Dealer;

namespace CarDealersWebApp.Services
{
    public interface ICarService
    {
        public Task CreateCarAsync(NewCarViewModel viewModel, string userEmail);
        public Task GetCarsAsync(List<ExistingCarViewModel> viewModels, string userEmail);
        public Task<bool> DeleteCarByIdAsync(int carId);
        public Task<bool> UpdateCarIdAsync(EditCarViewModel viewModel);
        public Task<Car?> GetCarByIdAsync(int carId);
        public string UploadImage(IFormFile file);
    }
}
