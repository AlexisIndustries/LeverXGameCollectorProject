using LeverXGameCollectorProject.Application.DTOs.Auth;
using LeverXGameCollectorProject.Domain.Entities.DB;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseModel> RegisterAsync(RegisterRequestModel request);
        Task<AuthResponseModel> LoginAsync(LoginRequestModel request);
        Task<UserProfileModel> GetUserProfileAsync(string userId);
        Task<AuthResponseModel> GenerateAuthResponse(UserEntity user);
        Task<UserEntity?> Login(LoginRequestModel login);
        Task<UserEntity> Register(RegisterRequestModel register);
    }
}
