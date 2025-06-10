using LeverXGameCollectorProject.Application.DTOs.Developer;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Commands
{
    public record CreateDeveloperCommand(CreateDeveloperRequestModel Request) : IRequest<int>;
}
