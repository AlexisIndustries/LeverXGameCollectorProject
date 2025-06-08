using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Features.Game.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GameResponseModel>>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GetAllGamesQueryHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameResponseModel>> Handle(GetAllGamesQuery request, CancellationToken ct)
        {
            var developers = await _service.GetAllGamesAsync();
            return developers.Select(_mapper.Map<GameResponseModel>);
        }
    }
}
