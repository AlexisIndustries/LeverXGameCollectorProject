using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Commands
{
    public record DeleteDeveloperCommand(int Id) : IRequest<Unit>;
}
