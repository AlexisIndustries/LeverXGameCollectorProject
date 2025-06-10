using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Unit>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
        {
            await _service.UpdateGenreAsync(command.Id, command.Request);
            return Unit.Value;
        }
    }
}
