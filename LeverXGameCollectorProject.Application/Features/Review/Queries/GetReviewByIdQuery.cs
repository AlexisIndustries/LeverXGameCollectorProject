using LeverXGameCollectorProject.Application.DTOs.Review;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Queries
{
    public record GetReviewByIdQuery(int Id) : IRequest<ReviewResponseModel>;
}
