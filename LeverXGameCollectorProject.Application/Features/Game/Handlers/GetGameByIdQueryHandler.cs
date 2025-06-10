using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Game.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Models.Game>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Models.Game> Handle(GetGameByIdQuery query, CancellationToken ct)
        {
            return await _service.GetGameByIdAsync(query.Id);
        }
    }
}
