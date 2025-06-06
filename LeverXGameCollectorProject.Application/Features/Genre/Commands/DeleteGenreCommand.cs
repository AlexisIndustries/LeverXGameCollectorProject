using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Commands
{
    public record DeleteGenreCommand(int Id) : IRequest<Unit>;
}
