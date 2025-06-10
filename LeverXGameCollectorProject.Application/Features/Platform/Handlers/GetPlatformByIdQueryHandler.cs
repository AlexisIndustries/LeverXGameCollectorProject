using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, Models.Platform>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public GetPlatformByIdQueryHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Models.Platform> Handle(GetPlatformByIdQuery query, CancellationToken ct)
        {
            return await _service.GetPlatformByIdAsync(query.Id);
        }
    }
}
