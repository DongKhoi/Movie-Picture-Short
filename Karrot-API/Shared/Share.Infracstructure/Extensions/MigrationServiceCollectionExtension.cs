using Authentication.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Share.Infracstructure.Extensions
{
    public static class MigrationServiceCollectionExtension
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var authDbContext = services.GetRequiredService<AuthenticationDbContext>();
                authDbContext.Database.Migrate();
            }
            return app;
        }
    }
}
