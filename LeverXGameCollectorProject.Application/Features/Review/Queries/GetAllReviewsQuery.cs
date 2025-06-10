using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Queries
{
    public record GetAllReviewQuery : IRequest<IEnumerable<Models.Review>>;
}
