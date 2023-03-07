using Application.Interfaces;
using Application.InternalServices;
using Application.IRepositories;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Bussiness
{
    public class OTPService : IOTPService
    {
        private readonly IOTPRepository _otpRepository;
        public OTPService(IOTPRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }
        public async Task<bool> GetOTPAsync(OtpDTO otpDTO)
        {
            return await _otpRepository.GetOTPAsync(otpDTO);
        }

        public async Task<Response<string>> CreateOTPAsync(string mailAddress)
        {
            if (mailAddress == null)
            {
                return Response<string>.Error("Email is required");
            }
            else
            {
                var existOTP = _otpRepository.ConfirmOTPAsync(mailAddress);
                if (existOTP.Result == false)
                {
                    Random rand = new Random();
                    int digits = rand.Next(10000, 100000);
                    var OTPcode = digits;
                    OTP otp = new OTP(new OtpDTO()
                    {
                        OTPcode = OTPcode,
                        MailAddress = mailAddress,
                    });
                    await _otpRepository.CreateOTPAsync(otp);

                    string body = "Verification code: " + digits;

                    await EmailService.SendAsync(mailAddress, "Verification Account Movie-Web", body);
                }
            }    

            return Response<string>.Success(mailAddress);
        }

        public async Task<bool> VerifyOTPAsync(OtpDTO otpDTO)
        {
            return await _otpRepository.GetOTPAsync(otpDTO);

        }
        public async Task<Response<string>> DeleteOTPAsync(string mailAddress)
        {
            await _otpRepository.DeleteOTPAsync(mailAddress);
            return Response<string>.Success(mailAddress);
        }
    }
}
