using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<Models.Genre>>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public GetAllGenreQueryHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Genre>> Handle(GetAllGenresQuery query, CancellationToken ct)
        {
            return await _service.GetAllGenresAsync();
        }
    }
}
