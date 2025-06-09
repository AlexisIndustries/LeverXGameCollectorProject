using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateGenreCommand request, CancellationToken ct)
        {
<<<<<<< HEAD
            var genre = _mapper.Map<Models.Genre>(request);
            await _repository.AddAsync(genre);
            return Unit.Value;
=======
            var genre = _mapper.Map<CreateGenreRequestModel>(request);
            var id = await _service.CreateGenreAsync(genre);
            return id;
>>>>>>> b96a2fe (Changed project structure)
        }
    }
}
