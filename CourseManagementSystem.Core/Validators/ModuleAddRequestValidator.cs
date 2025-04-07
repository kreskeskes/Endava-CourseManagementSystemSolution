using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.DTOs;
using FluentValidation;

namespace CourseManagementSystem.Core.Validators
{
    public class ModuleAddRequestValidator : AbstractValidator<ModuleAddRequest>
    {
        public ModuleAddRequestValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .Length(2, 80).WithMessage("Title length should be in between 2 and 80 characters long.");

            RuleFor(c => c.Order)
                .NotEmpty().WithMessage("Order cannot be empty.")
                .GreaterThan(0).WithMessage("Order must be greater than 0");

            RuleFor(c => c.CourseId)
                .NotEmpty().WithMessage("Course ID cannot be empty.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .Length(2, 300).WithMessage("Description length should be in between 2 and 300 characters long.");

            RuleFor(c => c.Content)
                .NotNull().WithMessage("Content cannot be empty.");

            RuleFor(c => c.CreatedBy)
                .NotEmpty().WithMessage("Creator cannot be empty.");

        }
    }
}
