using Document.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document.Data.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureUserContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(x => x.Id);
                user.HasIndex(x => x.Username).IsUnique();
                user.Property(x => x.Password).IsRequired();
            });
        }

        public static void ConfigureFileContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>(file =>
            {
                file.HasKey(x => x.Id);
                file.HasIndex(x => x.Name).IsUnique();
                file.Property(x => x.Name).IsRequired();
            });
        }
    }
}
