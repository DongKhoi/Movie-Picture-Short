
using Domain.Common;
using Domain.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

        public string UserName { get; protected set; }

        public string Password { get; protected set; }

        public string Email { get; protected set; }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public Role Role { get; protected set; }

        public RecoveryToken RecoveryToken { get; set; }


        private List<ReactionMovie> _reactionMovie;

        public IReadOnlyCollection<ReactionMovie> ReactionMovies => _reactionMovie;

        private User() 
        {
            Id = Guid.NewGuid();
            _reactionMovie = new List<ReactionMovie>();
        }

        public User(UserDTO userDTO) : this()
        {
            UserName = userDTO.UserName;
            Password = userDTO.Password;
            Email = userDTO.Email;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            Role = Role.Guest;
        }
    }
}
