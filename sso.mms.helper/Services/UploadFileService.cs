using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using sso.mms.helper;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using sso.mms.helper.ViewModels;

namespace sso.mms.helper.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        public UploadFileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, HttpClient httpClient)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
            this.httpClient = httpClient;
        }


        public bool IsUpload(List<IFormFile> formFiles) => formFiles != null && formFiles.Sum(f => f.Length) > 0;




        public async Task<List<string>> UploadImages(List<IFormFile> formFiles ,string? folderName)
        {
            Console.WriteLine("formFiles", formFiles);
            string uploadPath;
            List<string> listFileName = new List<string>();
            if (String.IsNullOrEmpty(folderName))
            {
                uploadPath = $"{webHostEnvironment.WebRootPath}/Files/";
            }
            else
            {
                uploadPath = $"{webHostEnvironment.WebRootPath}/Files/{folderName}/";
            }

            Console.WriteLine("uploadPath", uploadPath);

            foreach (var formFile in formFiles)
            {
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    string fullPath = uploadPath + fileName;

                    FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                    formFile.CopyTo(fileStream);

                    listFileName.Add(fileName);
                }
            }
            return listFileName;
        }

        public string Validation(List<IFormFile> formFiles)
        {
            foreach (var formFile in formFiles)
            {
                if (!ValidationExtension(formFile.FileName))
                {
                    return "Invalid file extension";
                }
                if (!ValidationSize(formFile.Length))
                {
                    return "The file is too large";
                }

            }
            return null;
        }

        public bool ValidationExtension(string fileName)
        {
            string[] permittedExtensions = { ".jpg", ".png", ".pdf",".jpeg" };
            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (String.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }
            return true;
        }

        bool ValidationSize(long fileSize)
        {
            //var fileSizeLimit = configuration.GetValue<long>("FileSizeLimit");
            return 10485760 > fileSize;
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(List<IFormFile> formFiles,string? folderName)
        {
            String errorMessage = String.Empty;
            String imageName = String.Empty;

            if (IsUpload(formFiles))
            {
                errorMessage = Validation(formFiles);

                if (String.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await UploadImages(formFiles, folderName))[0];
                }

            }
            return (errorMessage, imageName);

        }

        public Task<(string errorMessage, string UploadImagePath)> UploadImage(string uploadImagePath)
        {
            throw new NotImplementedException();
        }
    }
}
