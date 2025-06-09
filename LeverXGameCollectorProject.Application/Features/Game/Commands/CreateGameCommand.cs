using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Commands
{
    public record CreateGameCommand : IRequest<int>
    {
        public string? Title { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string? Description { get; init; }
        public int? Developer { get; init; }
        public int? Platform { get; init; }
        public int? Genre { get; init; }
    }
}
