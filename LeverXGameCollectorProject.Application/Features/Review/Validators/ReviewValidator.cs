using FluentValidation;
using LeverXGameCollectorProject.Application.DTOs.Review;
namespace LeverXGameCollectorProject.Application.Features.Review.Validators
{
    public class CreateReviewRequestModelValidator : AbstractValidator<CreateReviewRequestModel>
    {
        public CreateReviewRequestModelValidator()
        {
            RuleFor(x => x.GameId)
                .GreaterThan(0).WithMessage("Game ID is required");

            RuleFor(x => x.ReviewerName)
                .NotEmpty().WithMessage("Reviewer name is required")
                .Length(2, 100).WithMessage("Reviewer name must be between 2-100 characters");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1-5 stars");

            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters")
                .Must(comment => !string.IsNullOrEmpty(comment) || comment.Length >= 10)
                .When(x => !string.IsNullOrEmpty(x.Comment))
                .WithMessage("Comment must be at least 10 characters if provided");

            RuleFor(x => x.ReviewDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Review date cannot be in the future");
        }
    }
}
