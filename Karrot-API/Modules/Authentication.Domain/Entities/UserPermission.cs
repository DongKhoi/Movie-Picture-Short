
namespace Authentication.Domain.Entities
{
    public class UserPermission : BaseEntity
    {
        public Guid UserId { get; protected set; }

        public User? User { get; protected set; }

        public Guid PermissionId { get; protected set; }

        public Permission? Permission { get; protected set; }
    }
}
