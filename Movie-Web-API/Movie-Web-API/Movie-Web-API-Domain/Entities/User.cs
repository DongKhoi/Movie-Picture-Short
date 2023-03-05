﻿
using Core.DTOs;
using Domain.Common;
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


        private List<ReactionMovie> _reactionMovie;

        public IReadOnlyCollection<ReactionMovie> ReactionMovies => _reactionMovie;

        private User() 
        {
            Id = Guid.NewGuid();
            _reactionMovie = new List<ReactionMovie>();
        }

        public User(UserDTO command) : this()
        {

        }
    }
}