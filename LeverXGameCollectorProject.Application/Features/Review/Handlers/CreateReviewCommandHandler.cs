using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateReviewCommand request, CancellationToken ct)
        {
<<<<<<< HEAD
            var review = _mapper.Map<Models.Review>(request);
            await _repository.AddAsync(review);
            return Unit.Value;
=======
            var review = _mapper.Map<CreateReviewRequestModel>(request);
            var id = await _service.CreateReviewAsync(review);
            return id;
>>>>>>> b96a2fe (Changed project structure)
        }
    }
}
