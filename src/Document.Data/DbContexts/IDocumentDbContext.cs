using Document.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace Document.Data.DbContexts
{
    public interface IDocumentDbContext
    {
        DbSet<File> Files { get; set; }

        DbSet<User> Users { get; set; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync();
    }
}
