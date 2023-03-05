using Domain.DTOs;
using Domain.Entities;

namespace Application.IRepositories
{
    public interface IOTPRepository
    {
        Task<bool> GetOTPAsync(OtpDTO otpDTO);
        Task<bool> ConfirmOTPAsync(string mailAddress);
        Task CreateOTPAsync(OTP otp);
        Task DeleteOTPAsync(string mailAddress);
    }
}
