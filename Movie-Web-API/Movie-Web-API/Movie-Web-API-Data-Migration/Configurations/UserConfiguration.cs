using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => new { c.Id });
            builder.HasMany(x => x.ReactionMovies)
               .WithOne(x => x.User)
               .HasForeignKey(x => new { x.UserId })
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
