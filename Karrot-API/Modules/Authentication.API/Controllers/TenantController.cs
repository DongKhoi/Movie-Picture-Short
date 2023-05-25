using Authentication.Application.Interfaces;
using Authentication.Core.Common.Response;
using Authentication.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [Authorize(Policy = "Authorize")]
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public async Task<Response<List<string>>> CreateAsync([FromBody] TenantRequest request)
        {
            return await _tenantService.CreateTenantAsync(request);
        }

        [HttpPost("mapping")]
        public async Task<Response<List<string>>> MappingAsync([FromBody] TenantMappingRequest request)
        {
            return await _tenantService.CreateTenantDomainAsync(request);
        }
    }
}
