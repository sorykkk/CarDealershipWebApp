namespace CarDealersWebApp.Data.Entities;

public class RentRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string? Description { get; set; }
    public bool Approved { get; set; }
}
