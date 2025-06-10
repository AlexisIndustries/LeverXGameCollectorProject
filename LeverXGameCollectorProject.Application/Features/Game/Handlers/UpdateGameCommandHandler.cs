using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Unit>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
        {
            await _service.UpdateGameAsync(command.Id, command.Request);
            return Unit.Value;
        }
    }
}
