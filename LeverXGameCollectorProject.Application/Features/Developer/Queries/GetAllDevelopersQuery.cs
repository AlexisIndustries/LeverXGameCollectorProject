using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Queries
{
    public record GetAllDevelopersQuery : IRequest<IEnumerable<Models.Developer>>;
}
