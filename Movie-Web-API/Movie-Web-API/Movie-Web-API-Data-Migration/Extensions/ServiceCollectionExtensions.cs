using Application.Bussiness;
using Application.Interfaces;
using Application.IRepositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie_Web_API_Data_Migration;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNpgsqlPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MovieWebApiDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IReactionMovieRepository, ReactionMovieRepository>();

            services.AddScoped<IMovieService, MovieService>();

            return services;
        }
    }
}
