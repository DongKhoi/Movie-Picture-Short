using System.ComponentModel.DataAnnotations;

namespace IdentitySPA.Models
{
    public class TenantDomain
    {
        [Key]
        public Guid Id { get; protected set; }

        public string? DomainName { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public Tenant? Tenant { get; protected set; }

        public TenantDomain()
        {
            Id = Guid.NewGuid();
            DomainName = string.Empty;
        }
    }
}
