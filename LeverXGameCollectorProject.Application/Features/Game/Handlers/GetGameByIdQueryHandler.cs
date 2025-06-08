using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Features.Game.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameResponseModel>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<GameResponseModel> Handle(GetGameByIdQuery request, CancellationToken ct)
        {
            var developer = await _service.GetGameByIdAsync(request.Id);
            return _mapper.Map<GameResponseModel>(developer);
        }
    }
}
