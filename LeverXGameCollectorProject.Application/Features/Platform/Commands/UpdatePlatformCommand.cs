using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Commands
{
    public record UpdatePlatformCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; init; }
        public string? Manufacturer { get; init; }
        public int ReleaseYear { get; init; }
    }
}
