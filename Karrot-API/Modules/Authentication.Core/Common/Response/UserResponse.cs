using Authentication.Core.Common.Enum;

namespace Authentication.Core.Common.Response
{
    public class UserResponse
    {
        public Guid? Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Role Role { get; set; }

        public string? Jwt { get; set; }

        public Guid? TenantId { get; set; }
    }
}
