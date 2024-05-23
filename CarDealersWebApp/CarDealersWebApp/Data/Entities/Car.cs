namespace CarDealersWebApp.Data.Entities;

public enum CarFuelType
{
    Petrol = 0,
    Diesel,
    Electric,
    Hybrid,
    Hydrogen,
    Ethanol,
    CompressedNaturalGas,
    LiquefiedPetroleumGas,
    Biodiesel
}
public class Car
{
    public int Id { get; set; }
    public int DealerId { get; set; }
    public string BrandName { get; set; }
    public string Model { get; set; }
    public int HP { get; set; }
    public int Year { get; set; }
    public CarFuelType FuelType { get; set; }
    public int Mileage { get; set; }
    public float Price {  get; set; }
    public string? Description { get; set; }

    //if the image is not provided, a default image will be given
    //public IFormFile ImageFile { get; set; }
    public string ImagePath {  get; set; }
}
