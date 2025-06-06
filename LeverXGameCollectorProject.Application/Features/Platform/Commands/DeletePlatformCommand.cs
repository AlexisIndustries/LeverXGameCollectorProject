using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Commands
{
    public record DeletePlatformCommand(int Id) : IRequest<Unit>;
}
