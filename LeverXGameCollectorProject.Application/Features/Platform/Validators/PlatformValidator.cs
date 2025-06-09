using FluentValidation;

namespace LeverXGameCollectorProject.Application.Features.Platform.Validators
{
    public class PlatformValidator : AbstractValidator<Models.Platform>
    {
        public PlatformValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Platform name is required")
                .Length(2, 100).WithMessage("Name must be between 2-100 characters");

            RuleFor(x => x.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer is required")
                .Length(2, 100).WithMessage("Manufacturer must be between 2-100 characters");

            RuleFor(x => x.ReleaseYear)
                .InclusiveBetween(1970, DateTime.UtcNow.Year + 2)
                .WithMessage($"Release year must be between 1970-{DateTime.UtcNow.Year + 2}");

            RuleFor(x => x.Name)
                .Must((platform, name) => !name.Contains(platform.Manufacturer))
                .WithMessage("Platform name cannot contain manufacturer name");
        }
    }
}
