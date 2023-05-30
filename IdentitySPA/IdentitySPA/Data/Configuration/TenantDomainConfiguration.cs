using IdentitySPA.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IdentitySPA.Data.Configuration
{
    public class TenantDomainConfiguration : IEntityTypeConfiguration<TenantDomain>
    {
        public void Configure(EntityTypeBuilder<TenantDomain> builder)
        {
            builder.HasKey(c => new { c.Id });
        }
    }
}
