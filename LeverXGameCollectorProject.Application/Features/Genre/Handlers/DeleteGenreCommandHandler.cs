using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Unit>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public DeleteGenreCommandHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            await _service.DeleteGenreAsync(request.Id);
            return Unit.Value;
        }
    }
}
