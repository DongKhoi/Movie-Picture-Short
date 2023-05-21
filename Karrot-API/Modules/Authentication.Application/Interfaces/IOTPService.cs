using Authentication.Core.Common.Response;
using Authentication.Domain.DTOs;

namespace Authentication.Application.Interfaces
{
    public interface IOTPService
    {
        Task<bool> GetOTPAsync(OTPRequest otpDTO);
        Task<Response<string>> CreateOTPAsync(string mailAddress);
        Task<bool> VerifyOTPAsync(OTPRequest otpDTO);
        Task<Response<string>> DeleteOTPAsync(string mailAddress);
    }
}
