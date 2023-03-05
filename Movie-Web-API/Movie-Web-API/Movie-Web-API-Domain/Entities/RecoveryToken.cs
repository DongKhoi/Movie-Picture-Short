
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class RecoveryToken : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        public string Token { get; protected set; }

        public DateTime ExpiredDate { get; protected set; }

        public Guid UserId { get; protected set; }

        public User User { get; protected set; }

    }
}
