using LeverXGameCollectorProject.Application.DTOs.Review;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Commands
{
    public record UpdateReviewCommand(int Id, UpdateReviewRequestModel Request) : IRequest<Unit>;
}
