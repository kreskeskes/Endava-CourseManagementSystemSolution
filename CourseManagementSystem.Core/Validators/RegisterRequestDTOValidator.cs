using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.DTOs.User;
using FluentValidation;

namespace CourseManagementSystem.Core.Validators
{
    public class RegisterRequestDTOValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestDTOValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email address cannot be empty")
               .EmailAddress().WithMessage("Email address should be in a proper email format");

            RuleFor(x => x.Phonenumber)
                .NotEmpty().WithMessage("Phonenumber cannot be empty")
                .Matches("^\\+\\d{1,3}(?:[\\s-]?\\d{2,4}){3,4}$").WithMessage("Phonenumber must be in a proper phonenumber format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .Matches(x=>x.ConfirmPassword);

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username cannot be empty");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password cannot be empty");
        }
    }
}
