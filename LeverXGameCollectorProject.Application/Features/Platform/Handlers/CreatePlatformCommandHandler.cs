using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, Unit>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public CreatePlatformCommandHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreatePlatformCommand request, CancellationToken ct)
        {
            var platform = _mapper.Map<CreatePlatformRequestModel>(request);
            await _service.CreatePlatformAsync(platform);
            return Unit.Value;
        }
    }
}
