namespace sso.mms.helper
{
    public interface IUploadFileService
    {
        bool IsUpload(List<IFormFile> formFiles);

        string Validation(List<IFormFile> formFiles);

        Task<List<string>> UploadImages(List<IFormFile> formFiles ,string? folderName);

        Task<(string errorMessage, string imageName)> UploadImage(List<IFormFile> formFiles, string? folderName);
    }
}
