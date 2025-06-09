using LeverXGameCollectorProject.Application.DTOs.Platform;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Commands
{
    public record CreatePlatformCommand(CreatePlatformRequestModel request) : IRequest<int>
    {
    }
}
