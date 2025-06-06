using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Platform.Handlers
{
    public class UpdateDeveloperCommandHandler : IRequestHandler<UpdatePlatformCommand, Unit>
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDeveloperCommandHandler(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePlatformCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(_mapper.Map<Models.Platform>(request));
            return Unit.Value;
        }
    }
}
