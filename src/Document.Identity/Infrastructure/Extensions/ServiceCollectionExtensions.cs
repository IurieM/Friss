using Document.Identity.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Document.Identity.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppIdentity(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AuthenticateUserCommand).GetTypeInfo().Assembly);
        }
    }
}
