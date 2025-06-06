using LeverXGameCollectorProject.Application.DTOs.Genre;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Queries
{
    public record GetGenreByIdQuery(int Id) : IRequest<GenreResponseModel>;
}
