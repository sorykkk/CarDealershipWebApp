using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Models.Dealer;

namespace CarDealersWebApp.Services;

public interface IRentRequestService
{
    Task <bool>GetReqListAsync(List<RentRequestViewModel> viewModels, string userEmail);
    Task MakeDecisionAsync(int id, DecisionType decision, string? description);
}
