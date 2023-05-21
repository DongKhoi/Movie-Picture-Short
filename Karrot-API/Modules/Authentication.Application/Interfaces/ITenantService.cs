using Authentication.Core.Common.Response;
using Authentication.Domain.DTOs;

namespace Authentication.Application.Interfaces
{
    public interface ITenantService
    {
        Task<Response<List<string>>> CreateTenantAsync(TenantRequest request);
        Task<Response<List<string>>> CreateTenantDomainAsync(TenantMappingRequest request);
        Task<Guid> GetTenantIdByDomain(string domain);
    }
}
