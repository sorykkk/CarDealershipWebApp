using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Models.Dealer;

public class RentRequestViewModel
{
    public int Id { get; set; }
    public Car Car { get; set; } = new Car();
    public User User { get; set; } = new User();
    public DateTime FromTime { get; set; }
    public DateTime ToTime { get; set; }
    public DateTime SendTime {  get; set; }
    public string? Description { get; set; }
    public DecisionType Decision {  get; set; } = DecisionType.Undecided;

}

public class IncomingRequestList
{
    public List<RentRequestViewModel> ExistingRequests { get; set; } = new List<RentRequestViewModel>();
}
