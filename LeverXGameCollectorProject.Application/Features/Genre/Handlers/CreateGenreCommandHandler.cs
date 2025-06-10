using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateGenreCommand command, CancellationToken ct)
        {
            var id = await _service.CreateGenreAsync(command.Request);
            return id;
        }
    }
}
