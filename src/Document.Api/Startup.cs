using Document.Api.Infrastructure.Extensions;
using Document.Api.Infrastructure.Filters;
using Document.Api.Services;
using Document.Api.Services.Factories;
using Document.Api.Settings;
using Document.Common;
using Document.Data.DbContexts;
using Document.Identity.Infrastructure.Extensions;
using FluentValidation.AspNetCore;
using Identity.Api.Validators;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;

namespace Document.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
             .ReadFrom.Configuration(configuration)
             .CreateLogger();
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthenticationSettings>(Configuration.GetSection("AuthenticationSettings"));
            services.Configure<FileStorageSettings>(Configuration.GetSection("FileStorageSettings"));

            services.AddScoped<IFileService, FileSystemService>();
            services.AddScoped<IFileServiceFactory, FileServiceFactory>();

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddAppIdentity();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelValidationFilter));
                var policy = new AuthorizationPolicyBuilder()
                   .RequireAuthenticatedUser()
                   .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AuthenticateUserValidator>());

            ConfigureAuth(services);
            ConfigureDatabase(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.SeedDatabase();

            app.UseCustomExceptionHandler();
            app.UseLoggingMiddleware();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            if (Env.IsEnvironment("testing"))
            {
                services.AddDbContext<DocumentDbContext>(options =>
                options.UseInMemoryDatabase("Document").ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
            }
            else
            {
                services.AddDbContext<DocumentDbContext>(options =>
               options.UseSqlServer(Configuration["ConnectionString"]));
            }
            services.AddScoped<IDocumentDbContext, DocumentDbContext>();
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            var auth = new AuthenticationSettings();
            Configuration.GetSection("AuthenticationSettings").Bind(auth);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = auth.Authority,
                    ValidAudience = auth.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.SecretKey))
                };
            });
        }
    }
}
