using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Configurations
{
    public class TenantMappingConfiguration : IEntityTypeConfiguration<TenantMapping>
    {
        public void Configure(EntityTypeBuilder<TenantMapping> builder)
        {
            builder.HasKey(c => new { c.Id });
            builder.HasMany(x => x.Tenants)
                  .WithOne(x => x.TenantMapping)
                  .HasForeignKey(x => new { x.TenantMappingId })
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
