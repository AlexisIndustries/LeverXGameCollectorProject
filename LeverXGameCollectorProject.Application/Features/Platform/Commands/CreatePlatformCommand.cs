using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Commands
{
    public record CreatePlatformCommand : IRequest<Unit>
    {
        public string? Name { get; init; }
        public string? Manufacturer { get; init; }
        public int ReleaseYear { get; init; }
    }
}
