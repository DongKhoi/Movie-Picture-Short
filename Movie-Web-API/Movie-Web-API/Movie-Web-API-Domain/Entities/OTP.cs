
using Domain.DTOs;

namespace Domain.Entities
{
    public class OTP : BaseEntity
    {
        public Guid Id { get; set; }
        public int OTPcode { get; set; }
        public string MailAddress { get; set; }
        private OTP() 
        {
            Id = Guid.NewGuid();
        }

        public OTP(OtpDTO otpDTO) : this ()
        {
            OTPcode = otpDTO.OTPcode;
            MailAddress = otpDTO.MailAddress;
        }
    }
}
