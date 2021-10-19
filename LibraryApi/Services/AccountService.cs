using LibraryApi.Entieties;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtSettings _jwtSettings;

        public AccountService(LibraryDbContext dbContext, IPasswordHasher<User> password, JwtSettings jwtSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = password;
            _jwtSettings = jwtSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext
                       .Users
                       .Include(r=>r.Role)
                       .FirstOrDefault(x => x.Email == dto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.Lastname}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtSettings.JwtExpiresDays);

            var token = new JwtSecurityToken(_jwtSettings.JwtIssuer,
                                             _jwtSettings.JwtIssuer,
                                             claims,
                                             expires: expires,
                                             signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                PasswordHash = dto.Password,
                RoleId = dto.RoleId
            };
            var hashedpass = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedpass;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
