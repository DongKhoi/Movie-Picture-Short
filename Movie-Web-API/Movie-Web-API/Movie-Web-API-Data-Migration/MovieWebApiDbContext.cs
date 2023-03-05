using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Movie_Web_API_Data_Migration
{
    public class MovieWebApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<ReactionMovie> ReactionMovies { get; set; }

        public DbSet<RecoveryToken> RecoveryTokens { get; set; }

        public DbSet<OTP> OTPs { get; set; }

        public MovieWebApiDbContext(DbContextOptions<MovieWebApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReactionMovieConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OTPConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
