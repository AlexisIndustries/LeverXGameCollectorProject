using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Commands
{
    public record CreateGenreCommand : IRequest<Unit>
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Popularity { get; init; }
    }
}
