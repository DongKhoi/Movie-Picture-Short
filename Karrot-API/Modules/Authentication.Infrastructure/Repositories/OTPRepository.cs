using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Domain.IRepositories;
using Authentication.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class OTPRepository : IOTPRepository
    {
        private readonly AuthenticationDbContext _dbContext;
        public OTPRepository(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetOTPAsync(OTPRequest otpDTO)
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
