using LeverXGameCollectorProject.Application.DTOs.Game;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Queries
{
    public record GetAllGamesQuery : IRequest<IEnumerable<GameResponseModel>>;
}
