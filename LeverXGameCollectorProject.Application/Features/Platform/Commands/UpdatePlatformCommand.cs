using LeverXGameCollectorProject.Application.DTOs.Platform;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Commands
{
    public record UpdatePlatformCommand(int Id, UpdatePlatformRequestModel Request) : IRequest<Unit>;
}
