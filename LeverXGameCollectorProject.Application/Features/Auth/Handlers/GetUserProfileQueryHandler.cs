using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Application.Features.Auth.Queries;
using LeverXGameCollectorProject.Domain.Entities.DB;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LeverXGameCollectorProject.Application.Features.Auth.Handlers
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileModel>
    {
        private readonly UserManager<UserEntity> _userManager;

        public GetUserProfileQueryHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserProfileModel> Handle(GetUserProfileQuery request, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return new UserProfileModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
    }
}
