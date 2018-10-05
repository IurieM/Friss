using Document.Api.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Document.Data.DbContexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Document.Api.Queries
{
    public class FileListQueryHandler : IRequestHandler<FileListQuery, List<FileListModel>>
    {
        private readonly IDocumentDbContext dbContext;

        public FileListQueryHandler(IDocumentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<FileListModel>> Handle(FileListQuery request, CancellationToken cancellationToken)
        {
            return dbContext.Files.Select(file => new FileListModel()
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size,
                UploadedBy = file.User.Username,
                UploadedDate = file.UploadedDate,
                LastAccessedDate = file.LastAccessedDate
            }).ToListAsync();
        }
    }
}
