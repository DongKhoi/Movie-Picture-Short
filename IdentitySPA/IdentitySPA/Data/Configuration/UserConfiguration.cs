using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IdentitySPA.Models;

namespace IdentitySPA.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(c => new { c.Id });
            builder.HasOne(a => a.Tenant)
            .WithOne(b => b.ApplicationUser)
            .HasForeignKey<ApplicationUser>(b => b.TenantId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
