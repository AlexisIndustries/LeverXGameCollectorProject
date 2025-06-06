using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, Unit>
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public DeleteGameCommandHandler(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
