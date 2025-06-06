using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Commands
{
    public record CreateDeveloperCommand : IRequest<Unit>
    {
        public string Name { get; init; }
        public string Country { get; init; }
        public string Website { get; init; }
        public DateTime Founded { get; init; }
    }
}
