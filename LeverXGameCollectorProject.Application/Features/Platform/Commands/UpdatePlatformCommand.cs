using LeverXGameCollectorProject.Application.DTOs.Platform;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Commands
{
    public record UpdatePlatformCommand(int id, UpdatePlatformRequestModel request) : IRequest<Unit>
    {
    }
}
