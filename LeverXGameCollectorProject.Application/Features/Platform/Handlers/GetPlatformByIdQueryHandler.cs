using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, PlatformResponseModel>
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public GetPlatformByIdQueryHandler(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PlatformResponseModel> Handle(GetPlatformByIdQuery request, CancellationToken ct)
        {
            var platform = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<PlatformResponseModel>(platform);
        }
    }
}
