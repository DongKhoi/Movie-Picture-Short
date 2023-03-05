using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Movie : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string PathFile { get; protected set; }

        public int Priority { get; protected set; }

        public int LikeNumber { get; protected set; }


        private List<ReactionMovie> _reactionMovie;

        public IReadOnlyCollection<ReactionMovie> ReactionMovies => _reactionMovie;

        private Movie()
        {
            Id = Guid.NewGuid();
            _reactionMovie = new List<ReactionMovie>();
        }

    }
}
