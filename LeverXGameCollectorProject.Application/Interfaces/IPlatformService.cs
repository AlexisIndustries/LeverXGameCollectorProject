using LeverXGameCollectorProject.Application.DTOs.Platform;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformResponseModel>> GetAllPlatformsAsync();
        Task<PlatformResponseModel> GetPlatformByIdAsync(int id);
        Task<int> CreatePlatformAsync(CreatePlatformRequestModel PlatformDto);
        Task UpdatePlatformAsync(int id, UpdatePlatformRequestModel PlatformDto);
        Task DeletePlatformAsync(int id);
    }
}
