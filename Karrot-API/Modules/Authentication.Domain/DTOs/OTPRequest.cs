using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.DTOs
{
    public class OTPRequest
    {
        public int OTPcode { get; set; }
        public string? MailAddress { get; set; }
    }
}
