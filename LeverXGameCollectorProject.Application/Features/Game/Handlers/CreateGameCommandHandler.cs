using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Unit>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateGameCommand request, CancellationToken ct)
        {
            var game = _mapper.Map<CreateGameRequestModel>(request);
            await _service.CreateGameAsync(game);
            return Unit.Value;
        }
    }
}
