using Authentication.Core.Common.Enum;
using Authentication.Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        [MaxLength(255)]
        public string UserName { get; protected set; }

        [MaxLength(255)]
        public string? Password { get; protected set; }

        [MaxLength(255)]
        public string Email { get; protected set; }

        [MaxLength(255)]
        public string? FirstName { get; protected set; }

        [MaxLength(255)]
        public string? LastName { get; protected set; }

        public Role Role { get; protected set; }

        public RecoveryToken? RecoveryToken { get; protected set; }

        public Guid TenantId { get; protected set; }

        public Tenant? Tenant { get; protected set; }

        public IList<UserPermission>? UserPermissions { get; protected set; }

        private User()
        {
            Id = Guid.NewGuid();
            UserName = string.Empty;
            Email = string.Empty;
        }

        public User(AuthenticateRequest request) : this()
        {
            if (string.IsNullOrEmpty(request.UserName)|| string.IsNullOrEmpty(request.Email))
            {
                throw new ArgumentNullException();
            }
            UserName = request.UserName;
            Password = request.Password;
            Email = request.Email;
            FirstName = request.FirstName;
            LastName = request.LastName;
            CreatedDate = DateTimeOffset.UtcNow;
            UpdatedDate = null;
            TenantId = request.TenantId;
            Role = Role.User;
        }
    }
}
