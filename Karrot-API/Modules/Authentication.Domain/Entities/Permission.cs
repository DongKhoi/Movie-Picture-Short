using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class Permission : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        public string? PermissionName { get; protected set; }

        public IList<UserPermission>? UserPermissions { get; protected set; }
    }
}
