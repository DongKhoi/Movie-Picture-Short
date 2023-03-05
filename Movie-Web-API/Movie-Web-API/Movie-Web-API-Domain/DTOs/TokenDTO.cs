using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTime ExpiredDate { get; set; }

        public Guid UserId { get; set; }
    }
}
