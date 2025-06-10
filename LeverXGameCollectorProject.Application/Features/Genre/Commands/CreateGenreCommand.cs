using LeverXGameCollectorProject.Application.DTOs.Genre;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Commands
{
    public record CreateGenreCommand(CreateGenreRequestModel Request) : IRequest<int>;
}
