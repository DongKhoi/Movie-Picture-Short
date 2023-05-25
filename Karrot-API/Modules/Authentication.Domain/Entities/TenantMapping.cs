using Authentication.Domain.DTOs;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Authentication.Domain.Entities
{
    public class TenantMapping : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        public string DomainName { get; protected set; }

        private List<Tenant> _tenants;

        public IReadOnlyCollection<Tenant> Tenants => _tenants;

        public TenantMapping()
        {
            Id = Guid.NewGuid();
            DomainName = string.Empty;
            _tenants = new List<Tenant>();
        }
        public TenantMapping(TenantMappingRequest request) : this()
        {
            if (string.IsNullOrEmpty(request.Domain))
            {
                throw new ArgumentNullException();
            }
            DomainName = request.Domain;
        }
    }
}
