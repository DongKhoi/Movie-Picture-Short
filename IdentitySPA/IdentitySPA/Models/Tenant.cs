using System.ComponentModel.DataAnnotations;

namespace IdentitySPA.Models
{
    public class Tenant
    {
        [Key]
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Key { get; protected set; }

        public virtual ApplicationUser? ApplicationUser { get; protected set; }

        private List<TenantDomain> _tenantDomains;

        public IReadOnlyCollection<TenantDomain> TenantDomains => _tenantDomains;

        public Tenant()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Key = string.Empty;
            _tenantDomains = new List<TenantDomain>();
        }
    }
}
