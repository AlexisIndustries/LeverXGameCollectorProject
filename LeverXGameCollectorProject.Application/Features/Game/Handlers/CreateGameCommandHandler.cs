using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Unit>
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateGameCommand request, CancellationToken ct)
        {
            var game = _mapper.Map<Models.Game>(request);
            await _repository.AddAsync(game);
            return Unit.Value;
        }
    }
}
