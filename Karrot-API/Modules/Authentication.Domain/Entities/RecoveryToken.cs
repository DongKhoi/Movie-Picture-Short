using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class RecoveryToken : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        public string Token { get; protected set; }

        public DateTime ExpiredDate { get; protected set; }

        public Guid UserId { get; protected set; }

        public User? User { get; protected set; }

        private RecoveryToken()
        {

            Id = Guid.NewGuid();
            Token = string.Empty;
            UserId = Guid.Empty;
        }

        public RecoveryToken(Guid userId, string token, DateTime expiredDate) : this()
        {
            UserId = userId;
            ExpiredDate = expiredDate;
            Token = token;
        }
    }
}
