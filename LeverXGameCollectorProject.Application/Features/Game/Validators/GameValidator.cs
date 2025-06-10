using FluentValidation;
using LeverXGameCollectorProject.Application.DTOs.Game;

namespace LeverXGameCollectorProject.Application.Features.Game.Validators
{
    public class GameValidator : AbstractValidator<CreateGameRequestModel>
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

            RuleFor(x => x.PlatformId)
                .GreaterThan(0).When(x => x.PlatformId != null)
                .WithMessage("Invalid Platform ID");

            RuleFor(x => x.GenreId)
                .GreaterThan(0).When(x => x.GenreId != null)
                .WithMessage("Invalid Genre ID");

            RuleFor(x => x.DeveloperId)
                .GreaterThan(0).When(x => x.DeveloperId != null)
                .WithMessage("Invalid Developer ID");
        }
    }
}
