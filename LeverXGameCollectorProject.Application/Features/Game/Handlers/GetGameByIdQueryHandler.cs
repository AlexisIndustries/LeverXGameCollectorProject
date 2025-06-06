using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Features.Game.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Game.Handlers
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameResponseModel>
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GameResponseModel> Handle(GetGameByIdQuery request, CancellationToken ct)
        {
            var developer = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<GameResponseModel>(developer);
        }
    }
}
