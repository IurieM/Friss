using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Document.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Document.Common.Exceptions;
using Document.Common;
using System.Net;
using Document.Api.Services.Factories;

namespace Document.Api.Commands
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, int>
    {
        private readonly IDocumentDbContext dbContext;
        private readonly IFileServiceFactory fileServiceFactory;

        public DeleteFileCommandHandler(IDocumentDbContext dbContext, IFileServiceFactory fileServiceFactory)
        {
            this.dbContext = dbContext;
            this.fileServiceFactory = fileServiceFactory;
        }

        public async Task<int> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileToRemove = await dbContext.Files.FirstOrDefaultAsync(f => f.Id == request.FileId);
            if (fileToRemove == null)
            {
                throw new AppException(Constants.ErrorCodes.FileNotFound, HttpStatusCode.NotFound);
            }
            fileServiceFactory.Intance.DeleteFile(fileToRemove.Name);
            dbContext.Files.Remove(fileToRemove);
            await dbContext.SaveChangesAsync();
            return request.FileId;
        }
    }
}
