using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class DeletePlatformCommandHandler : IRequestHandler<DeletePlatformCommand, Unit>
    {
        private readonly IPlatformService _service;
        private readonly IMapper _mapper;

        public DeletePlatformCommandHandler(IPlatformService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePlatformCommand request, CancellationToken cancellationToken)
        {
            await _service.DeletePlatformAsync(request.Id);
            return Unit.Value;
        }
    }
}
