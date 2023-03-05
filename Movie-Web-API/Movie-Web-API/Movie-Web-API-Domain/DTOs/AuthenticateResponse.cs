using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.DTOs
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticateResponse(string id, string firstName, string lastName, bool active, string jwtToken, string refreshToken)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
            Active = active;
        }
    }
}
