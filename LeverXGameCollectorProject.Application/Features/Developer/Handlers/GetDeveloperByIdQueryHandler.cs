using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.Features.Developer.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetDeveloperByIdQueryHandler : IRequestHandler<GetDeveloperByIdQuery, DeveloperResponseModel>
    {
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public GetDeveloperByIdQueryHandler(IGameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeveloperResponseModel> Handle(GetDeveloperByIdQuery request, CancellationToken ct)
        {
            var developer = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<DeveloperResponseModel>(developer);
        }
    }
}
