using LeverXGameCollectorProject.Application.DTOs.Auth;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Auth.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileModel>
    {
        public string UserId { get; init; }
    }
}
