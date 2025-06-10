using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Queries
{
    public record GetGameByIdQuery(int Id) : IRequest<Models.Game>;
}
