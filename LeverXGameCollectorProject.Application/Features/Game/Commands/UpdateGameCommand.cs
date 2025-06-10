using LeverXGameCollectorProject.Application.DTOs.Game;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Commands
{
    public record UpdateGameCommand(int id, UpdateGameRequestModel request) : IRequest<Unit>
    {
    }
}
