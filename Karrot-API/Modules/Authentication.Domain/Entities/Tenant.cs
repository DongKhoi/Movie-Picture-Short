using Authentication.Core.Common.Enum;
using Authentication.Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Key { get; protected set; }

        public Guid? TenantMappingId { get; protected set; }

        public TenantMapping? TenantMapping { get; protected set; }
        private Tenant()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Key = string.Empty;
        }

        public Tenant(TenantRequest request) : this()
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Key))
            {
                throw new ArgumentNullException();
            }
            Name = request.Name;
            Key = request.Key;
            CreatedDate = DateTimeOffset.UtcNow;
            UpdatedDate = null;
        }
        public void Update(Guid tenantMappingId)
        {
            TenantMappingId = tenantMappingId;
        }
    }
}
