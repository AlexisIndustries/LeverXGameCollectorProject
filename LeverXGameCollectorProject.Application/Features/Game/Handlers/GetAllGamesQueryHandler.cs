using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Models.Game>>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GetAllGamesQueryHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Game>> Handle(GetAllGamesQuery query, CancellationToken ct)
        {
            return await _service.GetAllGamesAsync();
        }
    }
}
