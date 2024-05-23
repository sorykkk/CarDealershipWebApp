
using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Models.Dealer;
using Google.Apis.Drive.v3.Data;

namespace CarDealersWebApp.Services;

public class CarService : ICarService
{

    private readonly ICarRepository carRepository;
    private readonly IUserRepository userRepository;

    public CarService(ICarRepository carRepository, IUserRepository userRepository)
    {
        
        this.carRepository = carRepository;
        this.userRepository = userRepository;
    }

    public async Task CreateCarAsync(NewCarViewModel viewModel, string userEmail)
    {
        
        Data.Entities.User? existingUser = await userRepository.GetUserByEmail(userEmail);

        if (existingUser == null)
        {
            throw new Exception("Such user does not exist");
        }

        var car = new Car
        {
            BrandName = viewModel.BrandName,
            Model = viewModel.Model,
            DealerId = existingUser.Id,
            Mileage = viewModel.Mileage,
            HP = viewModel.HP,
            Year = viewModel.Year,
            Price = viewModel.Price,
            FuelType = (CarFuelType)viewModel.FuelType,
            Description = viewModel.Description,
            ImagePath = viewModel.ImagePath
        };

        await carRepository.SaveCar(car, existingUser.Id);
    }

    public async Task GetCarsAsync(List<ExistingCarViewModel> viewModels, string userEmail)
    {
        var cars = await carRepository.GetCarListOfUser(userEmail);

        foreach(var car in cars)
        {
            ExistingCarViewModel viewModel = new ExistingCarViewModel();
            viewModel.BrandName = car.BrandName;
            viewModel.Model = car.Model;
            viewModel.Year = car.Year;
            viewModel.Price = car.Price;
            viewModel.Mileage = car.Mileage;
            viewModel.ImagePath = car.ImagePath;

            if (!string.IsNullOrEmpty(car.Description) && car.Description.Length > 20)
            {
                viewModel.ShortDescription = car.Description.Substring(0, 20) + "...";
            }
            else
            {
                viewModel.ShortDescription = car.Description;
            }

            viewModels.Add(viewModel);
        }
    }

}
