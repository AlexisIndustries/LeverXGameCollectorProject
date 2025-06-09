using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Commands
{
    public record CreateReviewCommand : IRequest<int>
    {
        public int GameId { get; init; }
        public string? ReviewerName { get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public DateTime ReviewDate { get; init; }
    }
}
