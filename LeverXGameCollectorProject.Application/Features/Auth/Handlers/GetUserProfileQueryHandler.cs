using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Auth.Handlers
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileModel>
    {
        private readonly IAuthService _service;

        public GetUserProfileQueryHandler(IAuthService service)
        {
            _service = service;
        }

        public async Task<UserProfileModel> Handle(GetUserProfileQuery query, CancellationToken ct)
        {
            return await _service.GetUserProfileAsync(query.UserId);
        }
    }
}
