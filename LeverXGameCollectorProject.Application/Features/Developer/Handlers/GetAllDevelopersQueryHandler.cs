using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.Features.Developer.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetAllDevelopersQueryHandler : IRequestHandler<GetAllDevelopersQuery, IEnumerable<DeveloperResponseModel>>
    {
        private readonly IDeveloperRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDevelopersQueryHandler(IDeveloperRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeveloperResponseModel>> Handle(GetAllDevelopersQuery request, CancellationToken ct)
        {
            var developers = await _repository.GetAllAsync();
            return developers.Select(_mapper.Map<DeveloperResponseModel>);
        }
    }
}
