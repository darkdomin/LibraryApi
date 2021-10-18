using FluentValidation;
using LibraryApi.Entieties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(LibraryDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(p=>p.Password);

            RuleFor(x=>x.Email).Custom((value,context)=>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
