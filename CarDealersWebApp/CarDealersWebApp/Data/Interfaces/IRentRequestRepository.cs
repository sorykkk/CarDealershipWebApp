using CarDealersWebApp.Data.Entities;

namespace CarDealersWebApp.Data.Interfaces;

public interface IRentRequestRepository
{
    Task<List<RentRequest>> GetRequestForDealerId(int id);
    Task MakeRequestDecision(int id, DecisionType decision, string? description);
}
