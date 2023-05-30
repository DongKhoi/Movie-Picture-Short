using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentitySPA.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("TenantId")]
        public Guid? TenantId { get; set; }

        public Tenant? Tenant { get; set; }
    }
}