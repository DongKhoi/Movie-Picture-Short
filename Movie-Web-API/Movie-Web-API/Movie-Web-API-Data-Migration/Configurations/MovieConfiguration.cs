
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(c => new { c.Id });
            builder.HasIndex(x => new { x.Id}).IsUnique();
            builder.HasMany(x => x.ReactionMovies)
               .WithOne(x => x.Movie)
               .HasForeignKey(x => new { x.MovieId })
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
