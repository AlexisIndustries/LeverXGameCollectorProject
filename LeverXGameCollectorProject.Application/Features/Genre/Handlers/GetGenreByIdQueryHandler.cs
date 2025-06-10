using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Models.Genre>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Models.Genre> Handle(GetGenreByIdQuery query, CancellationToken ct)
        {
            return await _service.GetGenreByIdAsync(query.Id);
        }
    }
}
