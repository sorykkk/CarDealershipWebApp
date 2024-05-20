
using CarDealersWebApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Dealer;

public class OfferListViewModel
{
    [Required(ErrorMessage ="Introduce the name and the model of the car")]
    public string BrandName { get; set; }

    [Required(ErrorMessage = "Introduce the name and the model of the car")]
    public string Model { get; set; }

    [Required(ErrorMessage ="Introduce the Horse Power of the car")]
    public int HP { get; set; }

    [Required(ErrorMessage ="Itroduce the year of the car")]
    public int Year { get; set; }

    [Required(ErrorMessage ="Introduce the type of fuel")]
    public CarFuelType FuelType { get; set; }

    [Required(ErrorMessage ="Introduce the mileage")]
    public int Mileage { get; set; }

    [Required(ErrorMessage ="Introduce the price of the car")]
    public float Price { get; set; }
    public string? Description { get; set; }

    //if the image is not provided, a default image will be given
    //public string? ImagePath { get; set; }

    //public string CarImageUrl {  get; set; }
    public IFormFile ImageFile { get; set; }

}
