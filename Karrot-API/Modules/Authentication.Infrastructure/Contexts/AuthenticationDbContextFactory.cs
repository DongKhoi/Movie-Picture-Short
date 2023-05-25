using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Contexts
{
    public class AuthenticationDbContextFactory : IDesignTimeDbContextFactory<AuthenticationDbContext>
    {
        public AuthenticationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"..\Authentication.API"))
                        .AddJsonFile("appsettings.json")
                        .Build();

            optionsBuilder
                .UseSqlServer(
                     configuration.GetConnectionString("Karrot"),
                    b => b.MigrationsAssembly(typeof(AuthenticationDbContextFactory).Assembly.FullName));
            return new AuthenticationDbContext(optionsBuilder.Options);
        }
    }
}
