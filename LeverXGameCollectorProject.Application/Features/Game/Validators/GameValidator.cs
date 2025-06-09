using FluentValidation;

namespace LeverXGameCollectorProject.Application.Features.Game.Validators
{
    public class GameValidator : AbstractValidator<Models.Game>
    {
        public GameValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Game title is required")
                .Length(2, 200).WithMessage("Title must be between 2-200 characters");

            RuleFor(x => x.ReleaseDate)
                .LessThan(DateTime.UtcNow.AddDays(1)).WithMessage("Release date cannot be in the future");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

            RuleFor(x => x.Platform.Id)
                .GreaterThan(0).When(x => x.Platform?.Id != null)
                .WithMessage("Invalid Platform ID");

            RuleFor(x => x.Genre.Id)
                .GreaterThan(0).When(x => x.Genre?.Id != null)
                .WithMessage("Invalid Genre ID");

            RuleFor(x => x.Developer.Id)
                .GreaterThan(0).When(x => x.Developer?.Id != null)
                .WithMessage("Invalid Developer ID");
        }
    }
}
