
using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Models.Dealer;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Hosting;

namespace CarDealersWebApp.Services;

public class CarService : ICarService
{

    private readonly ICarRepository carRepository;
    private readonly IUserRepository userRepository;
    private readonly IWebHostEnvironment webHostEnvironment;

    public CarService(ICarRepository carRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
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
            viewModel.CarId = car.Id;
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

    public async Task<bool> DeleteCarByIdAsync(int carId)
    {
        if(carId < 1)
            return false;
        bool success = await carRepository.DeleteCar(carId);
        return success;
    }

    public async Task<Car?> GetCarByIdAsync(int carId)
    {
        return await carRepository.GetCarById(carId);
    }

    public async Task<bool> UpdateCarIdAsync(EditCarViewModel viewModel)
    {
        var car = await carRepository.GetCarById(viewModel.Id);
        if(car is null)
        {
            return false;
        }

        car.Id = viewModel.Id;
        car.BrandName = viewModel.BrandName;
        car.Model = viewModel.Model;
        car.Mileage = viewModel.Mileage;
        car.HP = viewModel.HP;
        car.Year = viewModel.Year;
        car.Price = viewModel.Price;
        car.FuelType = (CarFuelType)viewModel.FuelType;
        car.Description = viewModel.Description;
        car.ImagePath = viewModel.ImagePath;


        await carRepository.UpdateCar(car);
        return true;
    }

    public string UploadImage(IFormFile file)
    {
        string wwwRootPath = webHostEnvironment.WebRootPath;
        string imagePath;

        if (file != null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string productPath = Path.Combine(wwwRootPath, @"Images\Car");

            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            imagePath = @"\Images\Car\" + fileName;
        }
        else
        {
            imagePath = @"\Images\Car\car_unknown.jpg";
        }

        return imagePath;
    }

}
