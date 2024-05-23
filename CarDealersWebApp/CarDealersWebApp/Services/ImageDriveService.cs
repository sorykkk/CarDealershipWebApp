﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using static Google.Apis.Drive.v3.DriveService;
using Microsoft.AspNetCore.StaticFiles;

namespace CarDealersWebApp.Services;

public class ImageDriveService : IImageDriveService
{
    private static DriveService GetService()
    {
        var tokenResponse = new TokenResponse
        {
            AccessToken = "ya29.a0AXooCgt16JUALedTHxaBX9IyTPoWsvbVzDBi4ObVZmrcQSP_5ML-voT485nXt-2IKFWb3oUjs7Sa8viXool1E9CokFJwu5fFHf2WFlsR9uoY4wJUGO2Qq3DbvhFoxdYfDuhtaUxGNq5cej1JxyTOgjfwjqjdPUHouKqyaCgYKAQoSARESFQHGX2MiVgimRlvUrriNQ4C48KcazA0171",
            RefreshToken = "1//04YJZoPTpY7StCgYIARAAGAQSNwF-L9Ir5-rK1fv38Dl9knbpGq6wJpMY08bCzfwZNKNpt992JeF1aPhDdAxLR-pjJJ-vlAdeRio",
        };
        var applicationName = "Web CarDealers";
        var username = "soryn.besleaga@gmail.com";

        var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = "1019373861129-qr6q2rslqh6qoi0illbggm654hjn3l0a.apps.googleusercontent.com",
                ClientSecret = "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"
            },
            Scopes = new[] { Scope.Drive },
            DataStore = new FileDataStore(applicationName)
        });

        var credential = new UserCredential(apiCodeFlow, username, tokenResponse);

        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = applicationName
        });

        return service;
    }
    public string CreateFolder(string parent, string folderName)
    {
        var service = GetService();
        var driveFolder = new Google.Apis.Drive.v3.Data.File();
        driveFolder.Name = folderName;
        driveFolder.MimeType = "application/vnd.google-apps.folder";
        driveFolder.Parents = new string[] { parent };
        var command = service.Files.Create(driveFolder);
        var file = command.Execute();
        return file.Id;
    }

    private static string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        string contentType;
        if(!provider.TryGetContentType(fileName, out contentType))
        {
            contentType = "aplication/octet-stream"; //default MIME
        }

        return contentType;
    }

    public string GetFolderId(string folderName)
    {
        // Initialize the Drive service
        DriveService service = GetService();

        // Define the query to search for the folder by name
        string query = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";

        try
        {
            // Perform the query to search for the folder
            var request = service.Files.List();
            request.Q = query;
            var response = request.Execute();

            // Check if the folder was found
            if (response.Files.Any())
            {
                // Return the ID of the first matching folder
                return response.Files.First().Id;
            }
            else
            {
                // Folder not found
                throw new Exception("Folder not found");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    public string UploadFile(Stream file, string fileName, string folder, string fileDescription)
    {
        DriveService service = GetService();


        var driveFile = new Google.Apis.Drive.v3.Data.File();
        driveFile.Name = fileName;
        driveFile.Description = fileDescription;
        driveFile.Parents = new string[] { folder };

        string fileMime = GetMimeType(fileName);
        driveFile.MimeType = fileMime;

        var request = service.Files.Create(driveFile, file, fileMime);
            request.Fields = "id";

        var response = request.Upload();
        if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
            throw response.Exception;

        return request.ResponseBody.Id;
    }

    public void DeleteFile(string fileId)
    {
        var service = GetService();
        var command = service.Files.Delete(fileId);
        var result = command.Execute();
    }

    public IEnumerable<Google.Apis.Drive.v3.Data.File> GetFiles(string folder)
    {
        var service = GetService();

        var fileList = service.Files.List();
        fileList.Q = $"mimeType!='application/vnd.google-apps.folder' and '{folder}' in parents";
        fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

        var result = new List<Google.Apis.Drive.v3.Data.File>();
        string pageToken = null;
        do
        {
            fileList.PageToken = pageToken;
            var filesResult = fileList.Execute();
            var files = filesResult.Files;
            pageToken = filesResult.NextPageToken;
            result.AddRange(files);
        } while (pageToken != null);


        return result;
    }

}

