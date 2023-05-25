
namespace Authentication.Domain.DTOs
{
    public class AuthenticateRequest
    {
        public Guid TenantId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Jwt { get; set; }

        public bool? IsSSO { get; set; }


    }
}
