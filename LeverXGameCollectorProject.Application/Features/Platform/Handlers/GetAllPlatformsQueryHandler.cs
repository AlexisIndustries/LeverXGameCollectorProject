using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class GetAllPlatformQueryHandler : IRequestHandler<GetAllPlatformsQuery, IEnumerable<PlatformResponseModel>>
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPlatformQueryHandler(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlatformResponseModel>> Handle(GetAllPlatformsQuery request, CancellationToken ct)
        {
            var platforms = await _repository.GetAllAsync();
            return platforms.Select(_mapper.Map<PlatformResponseModel>);
        }
    }
}
