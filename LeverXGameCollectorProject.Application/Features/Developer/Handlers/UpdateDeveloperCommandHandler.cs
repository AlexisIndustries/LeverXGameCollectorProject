using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class UpdateDeveloperCommandHandler : IRequestHandler<UpdateDeveloperCommand, Unit>
    {
        private readonly IDeveloperRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDeveloperCommandHandler(IDeveloperRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(_mapper.Map<Models.Developer>(request));
            return Unit.Value;
        }
    }
}
