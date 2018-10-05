using Document.Data.Entities;
using Document.Data.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Document.Data.DbContexts
{
    public class DocumentDbContext : DbContext, IDocumentDbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options)
                : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserContext();
            modelBuilder.ConfigureFileContext();
            base.OnModelCreating(modelBuilder);
        }
    }
}
