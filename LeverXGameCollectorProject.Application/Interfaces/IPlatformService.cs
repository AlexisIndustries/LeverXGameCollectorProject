using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<Platform>> GetAllPlatformsAsync();
        Task<Platform> GetPlatformByIdAsync(int id);
        Task<int> CreatePlatformAsync(CreatePlatformRequestModel PlatformDto);
        Task UpdatePlatformAsync(int id, UpdatePlatformRequestModel PlatformDto);
        Task DeletePlatformAsync(int id);
    }
}
