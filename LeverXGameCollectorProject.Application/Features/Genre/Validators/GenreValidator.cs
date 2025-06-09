using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.Features.Genre.Validators
{
    public class GenreValidator : AbstractValidator<Models.Genre>
    {
        public GenreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Genre name is required")
                .Length(2, 50).WithMessage("Name must be between 2-50 characters")
                .Must(name => !name.Contains(" ")).WithMessage("Genre name cannot contain spaces");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Popularity)
                .Must(BeValidPopularityLevel).WithMessage("Invalid popularity level. Allowed: Low, Medium, High");
        }

        private bool BeValidPopularityLevel(string? popularity)
        {
            if (string.IsNullOrEmpty(popularity)) return true;
            var allowed = new[] { "Low", "Medium", "High" };
            return allowed.Contains(popularity, StringComparer.OrdinalIgnoreCase);
        }
    }
}
