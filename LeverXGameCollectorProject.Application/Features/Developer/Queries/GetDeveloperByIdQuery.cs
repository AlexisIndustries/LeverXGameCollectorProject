using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Queries
{
    public record GetDeveloperByIdQuery(int Id) : IRequest<Models.Developer>;
}
