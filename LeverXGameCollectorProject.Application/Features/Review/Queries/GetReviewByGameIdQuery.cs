using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Queries
{
    public record GetReviewByGameIdQuery(int Id) : IRequest<Models.Review>;
}
