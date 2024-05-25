using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Models.Dealer;
using Microsoft.AspNetCore.Mvc;

namespace CarDealersWebApp.Services;

public class RentRequestService : IRentRequestService
{
    private readonly ICarRepository carRepository;
    private readonly IUserRepository userRepository;
    private readonly IRentRequestRepository reqRepository;

    public RentRequestService(ICarRepository carRepository, IUserRepository userRepository, IRentRequestRepository reqRepository)
    {
        this.carRepository = carRepository;
        this.userRepository = userRepository;
        this.reqRepository = reqRepository; 
    }

    public async Task<bool> GetReqListAsync(List<RentRequestViewModel> viewModels, string userEmail)
    {
        User? user =  await userRepository.GetUserByEmail(userEmail);
        if (user == null)
        {
            return false;
        }
        var reqs = await reqRepository.GetRequestForDealerId(user.Id);
        
        foreach (var req in reqs)
        {
            RentRequestViewModel viewModel = new RentRequestViewModel();
            viewModel.Id = req.Id;
            viewModel.FromTime = req.FromTime;
            viewModel.ToTime = req.ToTime;
            viewModel.SendTime = req.SendTime;
            viewModel.Description = req.Description;
            viewModel.Decision = req.Decision;
            viewModel.Car = await carRepository.GetCarById(req.CarId);
            viewModel.User = await userRepository.GetUser(req.CustomerId);

            viewModels.Add(viewModel);
        }
        return true;
    }

}
