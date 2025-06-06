using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Unit>
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(_mapper.Map<Models.Game>(request));
            return Unit.Value;
        }
    }
}
