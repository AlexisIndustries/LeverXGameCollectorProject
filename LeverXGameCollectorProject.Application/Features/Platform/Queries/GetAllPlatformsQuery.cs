using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Queries
{
    public record GetAllPlatformsQuery : IRequest<IEnumerable<Models.Platform>>;
}
