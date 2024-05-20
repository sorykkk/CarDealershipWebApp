using CarDealersWebApp.Models.Auth;
using CarDealersWebApp.Models.Dealer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarDealersWebApp.Services;

namespace CarDealersWebApp.Controllers;

//[Authorize(Policy = "DealerOnly")]
public class OfferListController : Controller
{
    public string ImageFolderDb = "ImageDataBase";
    private IImageDriveService imageDriveService;

    public OfferListController(IImageDriveService imageDriveService)
    {
        this.imageDriveService = imageDriveService;
    }

    [HttpGet]
    public async Task<IActionResult> OfferList(OfferListViewModel viewModel)
    {
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(OfferListViewModel viewModel)
    {
        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if(viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
        {
            var folderId = ImageFolderDb;
            var fileDescription = "Upload via CarDealers for ImageDatabase";

            using (var stream = viewModel.ImageFile.OpenReadStream())
            {
                var fileId = imageDriveService.UploadFile(stream, viewModel.ImageFile.FileName, folderId, fileDescription);
                ViewBag.Message = "File uploaded successfully. File ID: " + fileId;
            }
        }
        else
        {
            ViewBag.Message = "No file selected for upload.";
        }

        return View(viewModel);

    }

}
