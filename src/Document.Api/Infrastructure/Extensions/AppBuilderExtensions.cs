using Document.Api.Infrastructure.Middlewares;
using Document.Data.DbContexts;
using Document.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Document.Api.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        private const string DbInMemoryProvider = "Microsoft.EntityFrameworkCore.InMemory";
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }

        public static void SeedDatabase(this IApplicationBuilder app)
        {
            //Setup Databases
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<IDocumentDbContext>();
                if (dbContext.Database.ProviderName != DbInMemoryProvider)
                {
                    dbContext.Database.Migrate();
                }
                if (!dbContext.Users.Any())
                {
                    dbContext.Users.AddRange(IdentitySeed.Users);
                    dbContext.SaveChangesAsync().Wait();
                }
            }
        }
    }
}
