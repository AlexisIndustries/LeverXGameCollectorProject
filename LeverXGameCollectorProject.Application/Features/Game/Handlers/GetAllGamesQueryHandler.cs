using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Features.Game.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GameResponseModel>>
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public GetAllGamesQueryHandler(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameResponseModel>> Handle(GetAllGamesQuery request, CancellationToken ct)
        {
            var developers = await _repository.GetAllAsync();
            return developers.Select(_mapper.Map<GameResponseModel>);
        }
    }
}
