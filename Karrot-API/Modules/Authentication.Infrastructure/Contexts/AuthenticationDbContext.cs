using Authentication.Domain.Entities;
using Authentication.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Contexts
{
    public class AuthenticationDbContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RecoveryToken> RecoveryTokens { get; set; }

        public DbSet<TenantMapping> TenantMappings { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<OTP> OTPs { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PermissionConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantMappingConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecoveryTokenConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserPermissionConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
