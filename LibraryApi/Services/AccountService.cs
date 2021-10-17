using LibraryApi.Entieties;
using LibraryApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(LibraryDbContext dbContext, IPasswordHasher<User> password)
        {
            _dbContext = dbContext;
            _passwordHasher = password;
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
