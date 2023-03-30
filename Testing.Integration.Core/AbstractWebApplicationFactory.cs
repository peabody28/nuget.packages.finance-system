using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Testing.Integration.Core
{
    public abstract class AbstractWebApplicationFactory<TProgram, TContext> : WebApplicationFactory<TProgram>
        where TProgram : class
        where TContext : DbContext
    {
        public string UserName { get; set; }

        public string UserRole { get; set; }

        protected virtual void RemoveDbConnectionService(IServiceCollection services)
        {
            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType.Equals(typeof(DbConnection)));

            services.Remove(dbConnectionDescriptor);
        }

        protected abstract void AddDbConnectionService(IServiceCollection services);

        protected virtual void RemoveDbContextService(IServiceCollection services)
        {
            var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType.Equals(typeof(DbContextOptions<TContext>)));

            services.Remove(dbContextDescriptor);
        }

        protected abstract void AddDbContextService(IServiceCollection services);

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Configure<TestAuthenticationSchemeOptions>(options =>
                {
                    options.UserName = UserName;
                    options.UserRole = UserRole;
                });

                RemoveDbContextService(services);
               
                RemoveDbConnectionService(services);
                AddDbConnectionService(services);

                AddDbContextService(services);

                // override authentication
                services.AddAuthentication(defaultScheme: TestAuthHandler.AuthenticationScheme)
                    .AddScheme<TestAuthenticationSchemeOptions, TestAuthHandler>(
                        TestAuthHandler.AuthenticationScheme, options => { });
            });

            builder.UseEnvironment("Development");
        }
    }
}
