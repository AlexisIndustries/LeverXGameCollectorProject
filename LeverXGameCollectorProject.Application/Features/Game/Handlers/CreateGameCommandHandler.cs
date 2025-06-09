using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateGameCommand request, CancellationToken ct)
        {
<<<<<<< HEAD
            var game = _mapper.Map<Models.Game>(request);
            await _repository.AddAsync(game);
            return Unit.Value;
=======
            var game = _mapper.Map<CreateGameRequestModel>(request);
            var id = await _service.CreateGameAsync(game);
            return id;
>>>>>>> b96a2fe (Changed project structure)
        }
    }
}
