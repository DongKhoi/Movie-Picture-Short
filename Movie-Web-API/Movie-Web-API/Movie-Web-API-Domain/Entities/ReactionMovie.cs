using Domain.Common;
using Domain.DTOs;

namespace Domain.Entities
{
    public class ReactionMovie : BaseEntity
    {
        public Guid UserId { get; protected set; }

        public User User { get; protected set; }

        public Guid MovieId { get; protected set; }

        public Movie Movie { get; protected set; }

        public Status Status { get; protected set; }

        private ReactionMovie() { }

        public ReactionMovie(ReactionMovieDTO reactionMovieDTO) : this()
        {
            UserId = reactionMovieDTO.UserId;
            MovieId = reactionMovieDTO.MovieId;
            Status = reactionMovieDTO.Status.Value;
        }
    }
}
