using LeverXGameCollectorProject.Application.DTOs.Developer;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Queries
{
    public record GetAllDevelopersQuery : IRequest<IEnumerable<DeveloperResponseModel>>;
}
