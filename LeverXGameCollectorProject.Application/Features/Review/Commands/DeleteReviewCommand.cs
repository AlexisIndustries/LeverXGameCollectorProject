using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Commands
{
    public record DeleteReviewCommand(int Id) : IRequest<Unit>;
}
