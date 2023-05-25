using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.Configurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(c => new { c.UserId, c.PermissionId });

            builder.HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserPermissions)
                .HasForeignKey(sc => sc.UserId);

            builder.HasOne<Permission>(sc => sc.Permission)
                .WithMany(s => s.UserPermissions)
                .HasForeignKey(sc => sc.PermissionId);
        }
    }
}
