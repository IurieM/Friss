using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Document.Data.DbContexts;
using Document.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Document.Common;
using Document.Api.Services;
using Document.Common.Exceptions;

namespace Document.Api.Commands
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, int>
    {
        private readonly IDocumentDbContext dbContext;
        private readonly IStoreFileService fileStorageService;

        public UploadFileCommandHandler(IDocumentDbContext dbContext, IStoreFileService fileStorageService)
        {
            this.dbContext = dbContext;
            this.fileStorageService = fileStorageService;
        }

        public async Task<int> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await FileNameIsUnique(request.File.FileName);

            await fileStorageService.UploadAsync(request.File);

            var newFile = new File()
            {
                UploadedDate = DateTime.UtcNow,
                Name = request.File.FileName,
                Size = request.File.Length,
                UserId = request.UserId,
                ContentType = request.File.ContentType
            };

            dbContext.Files.Add(newFile);
            await dbContext.SaveChangesAsync();

            return newFile.Id;
        }

        private async Task FileNameIsUnique(string fileName)
        {
            var duplicateFileName = await dbContext.Files.AnyAsync(file => file.Name.Equals(fileName));
            if (duplicateFileName)
            {
                throw new AppException(Constants.ErrorCodes.DuplicateFileName);
            }
        }
    }
}
