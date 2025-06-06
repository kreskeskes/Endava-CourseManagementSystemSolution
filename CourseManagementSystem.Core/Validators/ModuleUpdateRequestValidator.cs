﻿using CourseManagementSystem.API.DTOs;
using FluentValidation;

namespace CourseManagementSystem.Core.Validators
{
    public class ModuleUpdateRequestValidator : AbstractValidator<ModuleUpdateRequest>
    {
        public ModuleUpdateRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Course ID is required.");

            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .Length(2, 80).WithMessage("Title length should be in between 2 and 80 characters long.");

            RuleFor(c => c.Order)
                .GreaterThan(0).WithMessage("Order must be greater than 0");

            RuleFor(c => c.CourseId)
                .NotEmpty().WithMessage("Course ID cannot be empty.");

            RuleFor(c => c.Description)
                .Length(2, 300).WithMessage("Description length should be in between 2 and 300 characters long.");

        }
    }
}
