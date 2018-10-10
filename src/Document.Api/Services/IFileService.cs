using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Document.Api.Services
{
    public interface IFileService
    {
        bool CanHandle(string storageType);
        Task UploadAsync(IFormFile formFile);
        Task<Stream> DownloadAsync(string fileName);
        void DeleteFile(string fileName);
    }
}
