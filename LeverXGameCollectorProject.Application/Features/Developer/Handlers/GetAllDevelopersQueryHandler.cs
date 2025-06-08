using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.Features.Developer.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetAllDevelopersQueryHandler : IRequestHandler<GetAllDevelopersQuery, IEnumerable<DeveloperResponseModel>>
    {
        private readonly IDeveloperService _service;
        private readonly IMapper _mapper;

        public GetAllDevelopersQueryHandler(IDeveloperService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeveloperResponseModel>> Handle(GetAllDevelopersQuery request, CancellationToken ct)
        {
            var developers = await _service.GetAllDevelopersAsync();
            return developers.Select(_mapper.Map<DeveloperResponseModel>);
        }
    }
}
