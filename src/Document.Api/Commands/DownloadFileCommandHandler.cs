using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Document.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using Document.Api.Services;
using Document.Api.Models;

namespace Document.Api.Commands
{
    public class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, DownloadFileModel>
    {
        private readonly IDocumentDbContext dbContext;
        private readonly IStoreFileService storeFileService;

        public DownloadFileCommandHandler(IDocumentDbContext dbContext, IStoreFileService storeFileService)
        {
            this.dbContext = dbContext;
            this.storeFileService = storeFileService;
        }

        public async Task<DownloadFileModel> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {

            var dbfile = await dbContext.Files.FirstOrDefaultAsync(file => file.Id == request.FileId);

            var fileContent = await storeFileService.DownloadAsync(dbfile.Name);

            dbfile.LastAccessedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return new DownloadFileModel()
            {
                Name = dbfile.Name,
                Content = fileContent,
                ContentType = dbfile.ContentType
            };
        }
    }
}
