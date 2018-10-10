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
using Document.Api.Services.Factories;

namespace Document.Api.Commands
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, int>
    {
        private readonly IDocumentDbContext dbContext;
        private readonly IFileServiceFactory fileServiceFactory;

        public UploadFileCommandHandler(IDocumentDbContext dbContext, IFileServiceFactory fileServiceFactory)
        {
            this.dbContext = dbContext;
            this.fileServiceFactory = fileServiceFactory;
        }

        public async Task<int> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await FileNameIsUnique(request.File.FileName);

            await fileServiceFactory.Intance.UploadAsync(request.File);

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
