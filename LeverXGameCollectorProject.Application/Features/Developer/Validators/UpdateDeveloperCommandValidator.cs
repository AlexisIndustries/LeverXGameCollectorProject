using FluentValidation;
using LeverXGameCollectorProject.Application.Features.Game.Commands;

namespace LeverXGameCollectorProject.Application.Features.Developer.Validators
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid Developer ID");
        }
    }
}
