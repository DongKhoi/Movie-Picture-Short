
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OTPConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.HasKey(c => new { c.Id });
        }
    }
}
