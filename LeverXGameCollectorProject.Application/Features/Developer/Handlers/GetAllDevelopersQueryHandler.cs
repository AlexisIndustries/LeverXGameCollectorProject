using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetAllDevelopersQueryHandler : IRequestHandler<GetAllDevelopersQuery, IEnumerable<Models.Developer>>
    {
        private readonly IDeveloperService _service;
        private readonly IMapper _mapper;

        public GetAllDevelopersQueryHandler(IDeveloperService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Developer>> Handle(GetAllDevelopersQuery query, CancellationToken ct)
        {
            return await _service.GetAllDevelopersAsync();
        }
    }
}
