using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetDeveloperByIdQueryHandler : IRequestHandler<GetDeveloperByIdQuery, Models.Developer>
    {
        private readonly IDeveloperService _service;
        private readonly IMapper _mapper;

        public GetDeveloperByIdQueryHandler(IDeveloperService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Models.Developer> Handle(GetDeveloperByIdQuery query, CancellationToken ct)
        {
            return await _service.GetDeveloperByIdAsync(query.Id);
        }
    }
}
