using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Unit>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateGenreCommand request, CancellationToken ct)
        {
            var genre = _mapper.Map<CreateGenreRequestModel>(request);
            await _service.CreateGenreAsync(genre);
            return Unit.Value;
        }
    }
}
