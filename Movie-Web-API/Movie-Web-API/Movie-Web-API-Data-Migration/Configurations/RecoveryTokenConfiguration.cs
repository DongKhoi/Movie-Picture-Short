
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class RecoveryTokenConfiguration : IEntityTypeConfiguration<RecoveryToken>
    {
        public void Configure(EntityTypeBuilder<RecoveryToken> builder)
        {
            builder.HasKey(c => new { c.Id });
            builder.HasOne<User>()
              .WithMany()
              .HasForeignKey(n => new { n.UserId})
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
