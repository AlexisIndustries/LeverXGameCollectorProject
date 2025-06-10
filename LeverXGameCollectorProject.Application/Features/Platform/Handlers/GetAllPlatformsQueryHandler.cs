using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class GetAllPlatformQueryHandler : IRequestHandler<GetAllPlatformsQuery, IEnumerable<Models.Platform>>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public GetAllPlatformQueryHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Platform>> Handle(GetAllPlatformsQuery request, CancellationToken ct)
        {
            return await _service.GetAllPlatformsAsync();
        }
    }
}
