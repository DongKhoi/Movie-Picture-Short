using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ReactionMovieDTO
    {
        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

        public Status? Status { get; set; }

    }
}
