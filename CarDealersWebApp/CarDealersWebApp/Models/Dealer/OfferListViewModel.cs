using Microsoft.AspNetCore.Authorization;

namespace CarDealersWebApp.Models.Dealer;

[Authorize(Policy = "DealerOnly")]
public class OfferListViewModel
{

}
