using Application.IRepositories;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movie_Web_API_Data_Migration;

namespace Infrastructure.Repositories
{
    public class OTPRepository : IOTPRepository
    {
        private readonly MovieWebApiDbContext _dbContext;
        public OTPRepository(MovieWebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetOTPAsync(OtpDTO otpDTO)
        {
            return await _dbContext.OTPs.AnyAsync(x => x.MailAddress == otpDTO.MailAddress && x.OTPcode == otpDTO.OTPcode);
        }

        public async Task<bool> ConfirmOTPAsync(string mailAddress)
        {
            return await _dbContext.OTPs.AnyAsync(x => x.MailAddress == mailAddress);
        }

        public async Task CreateOTPAsync(OTP otp)
        {
            _dbContext.OTPs.Add(otp);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOTPAsync(string mailAddress)
        {
            var listOTPMail = await _dbContext.OTPs.Where(x => x.MailAddress == mailAddress).ToListAsync();
            _dbContext.OTPs.RemoveRange(listOTPMail);
            await _dbContext.SaveChangesAsync();
        }
    }
}
