using LeverXGameCollectorProject.Application.DTOs.Genre;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Commands
{
    public record UpdateGenreCommand(int Id, UpdateGenreRequestModel Request) : IRequest<Unit>;
}
