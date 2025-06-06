using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Commands
{
    public record UpdateGenreCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Popularity { get; init; }
    }
}
