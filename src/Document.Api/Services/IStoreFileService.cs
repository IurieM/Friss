using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Document.Api.Services
{
    public interface IStoreFileService
    {
        bool CanUpload(string storageType);
        Task UploadAsync(IFormFile formFile);
        Task<Stream> DownloadAsync(string fileName);
        void DeleteFile(string fileName);
    }
}
