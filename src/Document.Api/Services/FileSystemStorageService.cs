using Document.Api.Settings;
using Document.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace Document.Api.Services
{
    public class FileSystemStorageService : IStoreFileService
    {
        private readonly IOptions<FileStorageSettings> fileStorage;

        public FileSystemStorageService(IOptions<FileStorageSettings> fileStorage)
        {
            this.fileStorage = fileStorage;
        }

        public bool CanUpload(string storageType)
        {
            return storageType == Constants.FileStorageType.FileSystem;
        }

        public async Task UploadAsync(IFormFile formFile)
        {
            var filePath = Path.Combine(fileStorage.Value.FileSystem.Path, formFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }

        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(fileStorage.Value.FileSystem.Path, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<Stream> DownloadAsync(string fileName)
        {
            var filePath = Path.Combine(fileStorage.Value.FileSystem.Path, fileName);
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
