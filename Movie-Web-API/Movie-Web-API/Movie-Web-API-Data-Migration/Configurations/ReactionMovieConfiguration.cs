using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ReactionMovieConfiguration : IEntityTypeConfiguration<ReactionMovie>
    {
        public void Configure(EntityTypeBuilder<ReactionMovie> builder)
        {
            builder.HasKey(c => new { c.UserId, c.MovieId });
        }
    }
}
