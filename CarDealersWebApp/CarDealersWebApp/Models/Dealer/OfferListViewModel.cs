
using CarDealersWebApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Dealer;

public class NewCarViewModel
{
    [Required(ErrorMessage = "Brand Name is required")]
    public string BrandName { get; set; }

    [Required(ErrorMessage = "Model is required")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Year is required")]
    [Range(1980, 2024, ErrorMessage = "Year must be between 1980 and 2024")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Mileage is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a non-negative number")]
    public int Mileage { get; set; }

    [Required(ErrorMessage = "HP is required")]
    [Range(0, int.MaxValue, ErrorMessage = "HP must be a non-negative number")]
    public int HP { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Price must be a non-negative number")]
    public float Price { get; set; }

    [Required(ErrorMessage = "Fuel Type is required")]
    public CarFuelType FuelType { get; set; }

    [MaxLength(255, ErrorMessage = "Description can't exceed 255 characters")]
    public string? Description { get; set; }

    public IFormFile? file { get; set; }
    public string? ImagePath { get; set; }
}

public class ExistingCarViewModel
{
    public int CarId { get; set; }
    public string ImagePath { get; set; }
    public string BrandName { get; set; }

    public string Model { get; set; }

    public int Year { get; set; }

    public int Mileage { get; set; }

    public float Price { get; set; }

    public string? ShortDescription { get; set; }
}


public class CarListViewModel
{
    public NewCarViewModel? NewCarViewModel { get; set; }
    public List<ExistingCarViewModel> ExistingCars { get; set; } = new List<ExistingCarViewModel>();
}