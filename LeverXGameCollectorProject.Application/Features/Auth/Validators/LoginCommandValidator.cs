using FluentValidation;
using LeverXGameCollectorProject.Application.DTOs.Auth;

namespace LeverXGameCollectorProject.Application.Features.Auth.Validators
{
    public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
