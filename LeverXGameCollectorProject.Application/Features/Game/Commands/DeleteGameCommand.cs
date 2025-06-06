using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Commands
{
    public record DeleteGameCommand(int Id) : IRequest<Unit>;
}
