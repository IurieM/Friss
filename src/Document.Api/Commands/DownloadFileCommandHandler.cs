using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Document.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using Document.Api.Models;
using Document.Api.Services.Factories;

namespace Document.Api.Commands
{
    public class DownloadFileCommandHandler : IRequestHandler<DownloadFileCommand, DownloadFileModel>
    {
        private readonly IDocumentDbContext dbContext;
        private readonly IFileServiceFactory fileServiceFactory;

        public DownloadFileCommandHandler(IDocumentDbContext dbContext, IFileServiceFactory fileServiceFactory)
        {
            this.dbContext = dbContext;
            this.fileServiceFactory = fileServiceFactory;
        }

        public async Task<DownloadFileModel> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {

            var dbfile = await dbContext.Files.FirstOrDefaultAsync(file => file.Id == request.FileId);

            var fileContent = await fileServiceFactory.Intance.DownloadAsync(dbfile.Name);

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
