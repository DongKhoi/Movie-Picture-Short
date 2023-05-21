
namespace Authentication.Domain.DTOs
{
    public class TenantMappingRequest
    {
        public string? Domain { get; set; }

        public Guid TenantId { get; set; }
    }
}
