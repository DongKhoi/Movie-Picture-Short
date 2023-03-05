using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Movie_Web_API.Controllers
{
    [Route("api/otps")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly IOTPService _otpService;
        public OTPController(IOTPService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("send-email-otp")]
        public async Task<ActionResult<Response<string>>> SendOTPByEmail([FromBody] OtpDTO otpDTO)
        {
            return await _otpService.CreateOTPAsync(otpDTO);
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<Response<string>>> VerifyOTP([FromBody] OtpDTO otpDTO)
        {
            var result = await _otpService.VerifyOTPAsync(otpDTO);
            if (result == true)
            {
                return await _otpService.DeleteOTPAsync(otpDTO.MailAddress);
            }    
            else
                return Response<string>.Error("OTP is not match");
        }

        [HttpDelete("expired-otp")]
        public async Task<ActionResult<Response<string>>> OTPExpried([FromBody] OtpDTO otpDTO)
        {
            return await _otpService.DeleteOTPAsync(otpDTO.MailAddress);
        }
    }
}
