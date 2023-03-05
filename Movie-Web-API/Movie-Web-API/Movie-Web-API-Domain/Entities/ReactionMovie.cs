
namespace Domain.Entities
{
    public class ReactionMovie : BaseEntity
    {
        public Guid UserId { get; protected set; }

        public User User { get; protected set; }

        public Guid MovieId { get; protected set; }

        public Movie Movie { get; protected set; }

    }
}
