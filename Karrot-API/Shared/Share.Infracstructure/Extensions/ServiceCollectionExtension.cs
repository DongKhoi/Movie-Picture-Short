using Authentication.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Share.Infracstructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSqlServerPersistence(this IServiceCollection services, string connectionString)
        {
            services = services.AddSqlServerAuthentication(connectionString);
            return services;
        }
    }
}
