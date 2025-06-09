using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, int>
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public CreatePlatformCommandHandler(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePlatformCommand request, CancellationToken ct)
        {
<<<<<<< HEAD
            var platform = _mapper.Map<Models.Platform>(request);
            await _repository.AddAsync(platform);
            return Unit.Value;
=======
            var platform = _mapper.Map<CreatePlatformRequestModel>(request);
            var id = await _service.CreatePlatformAsync(platform);
            return id;
>>>>>>> b96a2fe (Changed project structure)
        }
    }
}
