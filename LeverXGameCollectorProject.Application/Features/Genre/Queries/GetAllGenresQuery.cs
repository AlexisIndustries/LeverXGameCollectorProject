using LeverXGameCollectorProject.Application.DTOs.Genre;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Queries
{
    public record GetAllGenresQuery : IRequest<IEnumerable<GenreResponseModel>>;
}
