using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Queries
{
    public record GetAllGamesQuery : IRequest<IEnumerable<Models.Game>>;
}
