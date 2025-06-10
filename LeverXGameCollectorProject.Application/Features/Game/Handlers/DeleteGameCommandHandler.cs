using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, Unit>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public DeleteGameCommandHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGameCommand command, CancellationToken cancellationToken)
        {
            await _service.DeleteGameAsync(command.Id);
            return Unit.Value;
        }
    }
}
