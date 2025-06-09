using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class UpdateDeveloperCommandHandler : IRequestHandler<UpdateDeveloperCommand, Unit>
    {
        private readonly IDeveloperService _service;
        private readonly IMapper _mapper;

        public UpdateDeveloperCommandHandler(IDeveloperService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _service.UpdateDeveloperAsync(request.id, request.request);
            return Unit.Value;
        }
    }
}
