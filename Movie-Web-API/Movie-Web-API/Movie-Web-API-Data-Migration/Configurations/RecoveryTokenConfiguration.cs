
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
            builder.HasOne(a => a.User)
            .WithOne(b => b.RecoveryToken)
            .HasForeignKey<RecoveryToken>(b => b.UserId);
        }
    }
}
