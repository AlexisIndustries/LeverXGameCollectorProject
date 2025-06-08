using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class GetAllPlatformQueryHandler : IRequestHandler<GetAllPlatformsQuery, IEnumerable<PlatformResponseModel>>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public GetAllPlatformQueryHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlatformResponseModel>> Handle(GetAllPlatformsQuery request, CancellationToken ct)
        {
            var platforms = await _service.GetAllPlatformsAsync();
            return platforms.Select(_mapper.Map<PlatformResponseModel>);
        }
    }
}
