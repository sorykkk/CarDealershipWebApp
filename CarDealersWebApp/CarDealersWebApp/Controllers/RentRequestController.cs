﻿using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Models.Dealer;
using CarDealersWebApp.Services;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;

namespace CarDealersWebApp.Controllers;

public class RentRequestController : Controller
{
    private readonly IRentRequestService reqService;

    public RentRequestController(IRentRequestService reqService)
    {
        this.reqService = reqService;
    }

    [HttpGet]
    public async Task<IActionResult> RentRequest()
    {
        var viewModel = new IncomingRequestList();
        var userEmail = HttpContext.Session.GetString("Email") ?? string.Empty;
        var getReq = await reqService.GetReqListAsync(viewModel.ExistingRequests, userEmail);
        if (!getReq)
            TempData["fail"] = "User could not be identified";
        

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> RentRequest(IncomingRequestList viewModel)
    {
        var userEmail = HttpContext.Session.GetString("Email") ?? string.Empty;
        bool getReq = false;
        if (!ModelState.IsValid)
        {
            getReq = await reqService.GetReqListAsync(viewModel.ExistingRequests, userEmail);
            if (!getReq)
                TempData["fail"] = "User could not be identified";
            return View(viewModel);
        }

        getReq = await reqService.GetReqListAsync(viewModel.ExistingRequests, userEmail);
        if (!getReq)
            TempData["fail"] = "User could not be identified";
        return RedirectToAction("RentRequest");
    }

    [HttpPost]
    public async Task<IActionResult> ApproveReq(int Id, string? decisionDescription)
    {
        await reqService.MakeDecisionAsync(Id, DecisionType.Approve, decisionDescription);
        TempData["success"] = "Request Approved";
        return RedirectToAction("RentRequest", "RentRequest");
    }

    [HttpPost]
    public async Task<IActionResult> RejectReq(int Id, string? decisionDescription)
    {
        await reqService.MakeDecisionAsync(Id, DecisionType.Reject, decisionDescription);
        TempData["success"] = "Reject Approved";
        return RedirectToAction("RentRequest", "RentRequest");
    }
}
