using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Queries
{
    public record GetGenreByIdQuery(int Id) : IRequest<Models.Genre>;
}
