using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, PlatformResponseModel>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public GetPlatformByIdQueryHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<PlatformResponseModel> Handle(GetPlatformByIdQuery request, CancellationToken ct)
        {
            var platform = await _service.GetPlatformByIdAsync(request.Id);
            return _mapper.Map<PlatformResponseModel>(platform);
        }
    }
}
