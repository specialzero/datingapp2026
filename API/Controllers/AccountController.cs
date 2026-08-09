using System.Text;
using ClassLib.Data.Contexts;
using ClassLib.Data.Entities;
using ClassLib.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ClassLib.Security.Services;
using ClassLib.Security.Models;
using ClassLib.Extensions;

namespace API.Controllers
{
    public class AccountController(DatingDbContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await CheckEmail(registerDto.Email))
                return BadRequest("Email address is already associated with an account");

            using var hmac = new HMACSHA512();

            var newUser = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            context.AppUsers.Add(newUser);
            await context.SaveChangesAsync();

           return newUser.ToDto(tokenService);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await context.AppUsers.SingleOrDefaultAsync(x => x.Email.ToLower() == loginDto.Email.ToLower());

            if (user is null)
                return Unauthorized("Invalid email address");

            var hmac = new HMACSHA512(user.PasswordSalt);
            var candidateHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            
            if (candidateHash.Length != user.PasswordHash.Length)
                return Unauthorized("invalid password1");
            
            for (int i = 0; i < candidateHash.Length; i++)
            {
                if (candidateHash[i] != user.PasswordHash[i])
                    return Unauthorized("invalid password2");
            }

            return user.ToDto(tokenService);
        }

        private async Task<bool> CheckEmail(string email)
        {
            return await context.AppUsers.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}