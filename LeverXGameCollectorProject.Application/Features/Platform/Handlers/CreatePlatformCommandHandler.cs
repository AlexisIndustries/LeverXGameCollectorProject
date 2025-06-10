using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, int>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public CreatePlatformCommandHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePlatformCommand command, CancellationToken ct)
        {
            var id = await _service.CreatePlatformAsync(command.Request);
            return id;
        }
    }
}
