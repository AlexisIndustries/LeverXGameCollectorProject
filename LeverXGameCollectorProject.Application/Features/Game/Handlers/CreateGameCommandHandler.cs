using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateGameCommand command, CancellationToken ct)
        {
            var id = await _service.CreateGameAsync(command.Request);
            return id;
        }
    }
}
