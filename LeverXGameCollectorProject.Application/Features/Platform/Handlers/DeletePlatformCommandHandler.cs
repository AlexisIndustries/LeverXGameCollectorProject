using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class DeletePlatformCommandHandler : IRequestHandler<DeletePlatformCommand, Unit>
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public DeletePlatformCommandHandler(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePlatformCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
