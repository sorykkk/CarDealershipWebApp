using CarDealersWebApp.Models.Auth;

namespace CarDealersWebApp.Services;

public interface IImageDriveService
{
    public string CreateFolder(string parent, string folderName);
    public string GetFolderId(string folderName);
    public string UploadFile(Stream file, string fileName, string folder, string fileDescription);
    public void DeleteFile(string fileId);
    public IEnumerable<Google.Apis.Drive.v3.Data.File> GetFiles(string folder);
}

