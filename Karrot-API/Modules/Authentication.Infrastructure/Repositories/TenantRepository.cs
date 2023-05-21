using Authentication.Domain.Entities;
using Authentication.Domain.IRepositories;
using Authentication.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AuthenticationDbContext _dbContext;
        public TenantRepository(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(Tenant tenant)
        {
            _dbContext.Tenants.Add(tenant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckTenantExistAsync(Guid tenant)
        {
            return await _dbContext.Tenants.AnyAsync(x => x.Id == tenant);
        }

        public async Task CreateTenantMappingAsync(TenantMapping tenant)
        {
            _dbContext.TenantMappings.Add(tenant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTenantMapping(Guid domainId, Guid tenantId)
        {
            var tenant = await _dbContext.Tenants.Where(x => x.Id == tenantId).SingleOrDefaultAsync();
            if (tenant != null) 
            {
                tenant.Update(domainId);
                _dbContext.Tenants.Update(tenant);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Guid> GetTenantIdByDomain(string domain)
        {
            var result = await _dbContext.Tenants.Join(_dbContext.TenantMappings, tenant => tenant.TenantMappingId, mapping => mapping.Id,
                        (tenant, mapping) => new { Tenant = tenant, Mapping = mapping })
                        .Where(x => x.Mapping.DomainName == domain).Select(x => x.Tenant).FirstOrDefaultAsync();
            if(result != null)
            {
                return result.Id;
            }
            return Guid.Empty;
        }
    }
}
