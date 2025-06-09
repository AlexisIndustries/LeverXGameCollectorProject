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
<<<<<<< HEAD
            var platform = await _repository.GetByIdAsync(request.Id);
=======
            var platform = await _service.GetPlatformByIdAsync(request.Id);
>>>>>>> b96a2fe (Changed project structure)
            return _mapper.Map<PlatformResponseModel>(platform);
        }
    }
}
