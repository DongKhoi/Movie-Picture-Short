using Authentication.Application.Interfaces;
using Authentication.Core.Common.Response;
using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Domain.IRepositories;

namespace Authentication.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Response<List<string>>> CreateTenantAsync(TenantRequest request)
        {
            var errorMessages = new List<string>();
            var messages = new List<string>();

            if (string.IsNullOrEmpty(request.Name))
                errorMessages.Add("Name is empty");

            if (string.IsNullOrEmpty(request.Key))
                errorMessages.Add("Key is empty");

            if (errorMessages.Count > 0)
                return Response<List<string>>.Error(errorMessages);
            else
            {
                var tenant = new Tenant(request);
                await _tenantRepository.CreateAsync(tenant);
                messages.Add("Create tenant successfully !");
            }    

            return Response<List<string>>.Success(messages);
        }
        public async Task<Response<List<string>>> CreateTenantDomainAsync(TenantMappingRequest request)
        {
            var errorMessages = new List<string>();
            var messages = new List<string>();

            if (string.IsNullOrEmpty(request.Domain))
                errorMessages.Add("Domain is empty");

            if (request.TenantId == Guid.Empty)
                errorMessages.Add("TenantId is empty");

            if (errorMessages.Count > 0)
                return Response<List<string>>.Error(errorMessages);
            else
            {
                var tenant = new TenantMapping(request);
                await _tenantRepository.CreateTenantMappingAsync(tenant);
                await _tenantRepository.UpdateTenantMapping(tenant.Id, request.TenantId);

                messages.Add("Mapping tenant-domain successfully !");
            }

            return Response<List<string>>.Success(messages);
        }

        public async Task<Guid> GetTenantIdByDomain(string domain)
        {
            return await _tenantRepository.GetTenantIdByDomain(domain);
        }
    }
}
