using Authentication.Domain.DTOs;

namespace Authentication.Domain.Entities
{
    public class OTP : BaseEntity
    {
        public Guid Id { get; set; }
        public int OTPcode { get; set; }
        public string MailAddress { get; set; }
        private OTP()
        {
            Id = Guid.NewGuid();
            MailAddress = string.Empty;
        }

        public OTP(OTPRequest otpDTO) : this()
        {
            if (string.IsNullOrEmpty(otpDTO.MailAddress))
            {
                throw new ArgumentNullException();
            }
            OTPcode = otpDTO.OTPcode;
            MailAddress = otpDTO.MailAddress;
        }
    }
}
