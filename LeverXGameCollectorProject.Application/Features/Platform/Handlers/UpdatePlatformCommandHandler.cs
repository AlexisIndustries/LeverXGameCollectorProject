using AutoMapper;
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

        public async Task<Unit> Handle(UpdatePlatformCommand command, CancellationToken cancellationToken)
        {
            await _service.UpdatePlatformAsync(command.Id, command.Request);
            return Unit.Value;
        }
    }
}
