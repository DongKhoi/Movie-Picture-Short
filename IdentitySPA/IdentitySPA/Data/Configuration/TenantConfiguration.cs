using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IdentitySPA.Models;

namespace IdentitySPA.Data.Configuration
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(c => new { c.Id });
            builder.HasMany(x => x.TenantDomains)
                  .WithOne(x => x.Tenant)
                  .HasForeignKey(x => new { x.TenantId })
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
