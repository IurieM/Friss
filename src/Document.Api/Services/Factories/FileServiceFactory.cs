using Document.Api.Settings;
using Document.Common;
using Document.Common.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Document.Api.Services.Factories
{
    public class FileServiceFactory : IFileServiceFactory
    {
        private readonly IEnumerable<IFileService> fileServices;
        private readonly IOptions<FileStorageSettings> fileStorage;

        public FileServiceFactory(IEnumerable<IFileService> fileServices, IOptions<FileStorageSettings> fileStorage)
        {
            this.fileServices = fileServices;
            this.fileStorage = fileStorage;
        }

        public IFileService Intance
        {
            get
            {
                var fileService = fileServices.FirstOrDefault(f => f.CanHandle(fileStorage.Value.StorageType));
                if (fileServices == null)
                {
                    var exception = new ArgumentNullException($"No file service can handle {fileStorage.Value.StorageType}");
                    throw new AppException(Constants.ErrorCodes.InternalServerError, System.Net.HttpStatusCode.InternalServerError, exception);
                }

                return fileService;
            }
        }
    }
}
