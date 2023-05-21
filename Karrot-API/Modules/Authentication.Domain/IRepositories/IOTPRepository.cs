using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Domain.IRepositories
{
    public interface IOTPRepository
    {
        Task<bool> GetOTPAsync(OTPRequest otpDTO);
        Task<bool> ConfirmOTPAsync(string mailAddress);
        Task CreateOTPAsync(OTP otp);
        Task DeleteOTPAsync(string mailAddress);
    }
}
