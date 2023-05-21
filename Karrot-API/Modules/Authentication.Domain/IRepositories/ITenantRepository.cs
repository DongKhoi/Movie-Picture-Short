using Authentication.Domain.Entities;

namespace Authentication.Domain.IRepositories
{
    public interface ITenantRepository
    {
        Task CreateAsync(Tenant tenant);
        Task<bool> CheckTenantExistAsync(Guid tenant);
        Task CreateTenantMappingAsync(TenantMapping tenant);
        Task UpdateTenantMapping(Guid domainId, Guid tenantId);
        Task<Guid> GetTenantIdByDomain(string domain);

    }
}
