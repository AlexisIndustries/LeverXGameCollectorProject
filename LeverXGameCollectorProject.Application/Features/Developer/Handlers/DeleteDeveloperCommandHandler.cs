using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class DeleteDeveloperCommandHandler : IRequestHandler<DeleteDeveloperCommand, Unit>
    {
        private readonly IDeveloperService _service;
        private readonly IMapper _mapper;

        public DeleteDeveloperCommandHandler(IDeveloperService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _service.DeleteDeveloperAsync(request.Id);
            return Unit.Value;
        }
    }
}
