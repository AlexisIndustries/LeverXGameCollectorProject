using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, int>
    {
        private readonly IDeveloperRepository _repository;
        private readonly IMapper _mapper;

        public CreateDeveloperCommandHandler(IDeveloperRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDeveloperCommand request, CancellationToken ct)
        {
<<<<<<< HEAD
            var developer = _mapper.Map<Models.Developer>(request);
            await _repository.AddAsync(developer);
            return Unit.Value;
=======
            var developer = _mapper.Map<CreateDeveloperRequestModel>(request);
            var id = await _service.CreateDeveloperAsync(developer);
            return id;
>>>>>>> b96a2fe (Changed project structure)
        }
    }
}
