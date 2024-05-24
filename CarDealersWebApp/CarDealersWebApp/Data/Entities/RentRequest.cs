namespace CarDealersWebApp.Data.Entities;

public enum DecisionType
{
    Reject = 0,
    Approve,
    Undecided
};


public class RentRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public DateTime SendTime { get; set; }//when the request was sent
    public string? Description { get; set; }
    public DecisionType Decision { get; set; }
}
