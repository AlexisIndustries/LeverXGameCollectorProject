using FluentValidation;
using LeverXGameCollectorProject.Application.DTOs.Developer;

namespace LeverXGameCollectorProject.Application.Features.Developer.Validators
{
    public class CreateDeveloperRequestModelValidator : AbstractValidator<CreateDeveloperRequestModel>
    {
        public CreateDeveloperRequestModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .Length(2, 100).WithMessage("Country must be between 2-100 characters");

            RuleFor(x => x.Website)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Invalid website URL");

            RuleFor(x => x.Founded)
                .LessThan(DateTime.UtcNow).WithMessage("Founded date cannot be in the future");
        }
    }
}
