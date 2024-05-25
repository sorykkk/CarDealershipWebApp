using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Models.Dealer;

public class RentRequestViewModel
{
    public int Id { get; set; }
    public Car Car { get; set; } = new Car();
    public string UserName { get; set; }
    public string UserPhone { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public DateTime SendTime {  get; set; }
    public string? Description { get; set; }
    public DecisionType Decision {  get; set; } = DecisionType.Undecided;

}

public class IncomingRequestList
{
    public List<RentRequestViewModel> ExistingRequests { get; set; } = new List<RentRequestViewModel>();
}
