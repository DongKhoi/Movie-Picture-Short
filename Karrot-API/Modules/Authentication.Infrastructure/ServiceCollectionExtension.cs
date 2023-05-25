using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.IRepositories;
using Authentication.Infrastructure.Contexts;
using Authentication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSqlServerAuthentication(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AuthenticationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IAuthenticationService, ValidateTokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IOTPService, OTPService>();

            services.AddScoped<IRecoveryTokenRepository, RecoveryTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IOTPRepository, OTPRepository>();
            return services;
        }
    }
}
