using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.Features.Developer.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetDeveloperByIdQueryHandler : IRequestHandler<GetDeveloperByIdQuery, DeveloperResponseModel>
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GetDeveloperByIdQueryHandler(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<DeveloperResponseModel> Handle(GetDeveloperByIdQuery request, CancellationToken ct)
        {
            var developer = await _service.GetGameByIdAsync(request.Id);
            return _mapper.Map<DeveloperResponseModel>(developer);
        }
    }
}
