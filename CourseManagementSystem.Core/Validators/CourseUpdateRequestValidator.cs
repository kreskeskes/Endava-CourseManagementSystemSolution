using CourseManagementSystem.API.DTOs.Course;
using FluentValidation;

namespace CourseManagementSystem.Core.Validators
{
    public class CourseUpdateRequestValidator : AbstractValidator<CourseUpdateRequest>
    {
        public CourseUpdateRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Course ID is required.");

            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .Length(2, 80).WithMessage("Title length should be in between 2 and 80 characters long.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .Length(2, 300).WithMessage("Description length should be in between 2 and 300 characters long.");

            RuleFor(c => c.Discipline)
                .NotNull().WithMessage("Discipline cannot be empty.")
                .IsInEnum().WithMessage("Invalid discipline.");

            RuleFor(c => c.Difficulty)
                .NotNull().WithMessage("Difficulty cannot be empty.")
                .IsInEnum().WithMessage("Invalid difficulty.");

        }
    }
}
