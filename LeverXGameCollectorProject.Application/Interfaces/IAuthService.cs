using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Domain.Entities.DB;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponceModel> RegisterAsync(RegisterRequestModel request);
        Task<AuthResponceModel> LoginAsync(LoginRequestModel request);
        Task<UserProfileModel> GetUserProfileAsync(string userId);
        Task<AuthResponceModel> GenerateAuthResponse(UserEntity user);
    }
}
