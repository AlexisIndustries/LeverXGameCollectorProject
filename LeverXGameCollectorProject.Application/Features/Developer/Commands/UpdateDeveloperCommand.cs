using LeverXGameCollectorProject.Application.DTOs.Developer;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Commands
{
    public record UpdateDeveloperCommand(int id, UpdateDeveloperRequestModel request) : IRequest<Unit>
    {
    }
}
