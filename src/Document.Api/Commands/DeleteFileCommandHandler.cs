using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Document.Data.DbContexts;
using Document.Api.Services;
using Microsoft.EntityFrameworkCore;
using Document.Common.Exceptions;
using Document.Common;
using System.Net;

namespace Document.Api.Commands
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, int>
    {
        private readonly IDocumentDbContext dbContext;
        private readonly IStoreFileService storeFileService;

        public DeleteFileCommandHandler(IDocumentDbContext dbContext, IStoreFileService storeFileService)
        {
            this.dbContext = dbContext;
            this.storeFileService = storeFileService;
        }

        public async Task<int> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileToRemove = await dbContext.Files.FirstOrDefaultAsync(f => f.Id == request.FileId);
            if (fileToRemove == null)
            {
                throw new AppException(Constants.ErrorCodes.FileNotFound, HttpStatusCode.NotFound);
            }
            storeFileService.DeleteFile(fileToRemove.Name);
            dbContext.Files.Remove(fileToRemove);
            await dbContext.SaveChangesAsync();
            return request.FileId;
        }
    }
}
