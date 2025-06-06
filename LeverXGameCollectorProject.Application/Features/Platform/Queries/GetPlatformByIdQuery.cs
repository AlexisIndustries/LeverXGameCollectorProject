using LeverXGameCollectorProject.Application.DTOs.Platform;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Queries
{
    public record GetPlatformByIdQuery(int Id) : IRequest<PlatformResponseModel>;
}
