using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class UpdateDeveloperCommandHandler : IRequestHandler<UpdatePlatformCommand, Unit>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public UpdateDeveloperCommandHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePlatformCommand request, CancellationToken cancellationToken)
        {
            await _service.UpdatePlatformAsync(request.Id, _mapper.Map<UpdatePlatformRequestModel>(request));
            return Unit.Value;
        }
    }
}
