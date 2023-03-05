using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Movie_Web_API_Data_Migration
{
    public class MovieWebApiDbContextFactory : IDesignTimeDbContextFactory<MovieWebApiDbContext>
    {
        public MovieWebApiDbContext CreateDbContext(string[] args)
        {    
            var optionsBuilder = new DbContextOptionsBuilder<MovieWebApiDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"..\Movie-Web-API"))
                          .AddJsonFile("appsettings.json")
                          .Build();

            optionsBuilder
                .UseNpgsql(
                     configuration.GetConnectionString("Movie-Web"),
                    b => b.MigrationsAssembly(typeof(MovieWebApiDbContextFactory).Assembly.FullName));
            return new MovieWebApiDbContext(optionsBuilder.Options);
        }
    }
}
