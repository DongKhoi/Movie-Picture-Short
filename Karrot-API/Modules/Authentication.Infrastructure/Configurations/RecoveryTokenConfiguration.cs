using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Configurations
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
