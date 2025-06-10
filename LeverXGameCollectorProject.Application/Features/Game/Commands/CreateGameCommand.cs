using LeverXGameCollectorProject.Application.DTOs.Game;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Commands
{
    public record CreateGameCommand(CreateGameRequestModel Request) : IRequest<int>;
}
