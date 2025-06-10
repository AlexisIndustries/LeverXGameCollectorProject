using LeverXGameCollectorProject.Application.DTOs.Game;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Commands
{
    public record UpdateGameCommand(int Id, UpdateGameRequestModel Request) : IRequest<Unit>;
}
