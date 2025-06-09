using LeverXGameCollectorProject.Application.DTOs.Review;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Commands
{
    public record UpdateReviewCommand(int id, UpdateReviewRequestModel request) : IRequest<Unit>
    {
        public int Id { get; set; }
        public int GameId { get; init; }
        public string? ReviewerName { get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public DateTime ReviewDate { get; init; }
    }
}
