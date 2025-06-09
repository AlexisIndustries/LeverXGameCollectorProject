using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformResponseModel>> GetAllPlatformsAsync();
        Task<PlatformResponseModel> GetPlatformByIdAsync(int id);
        Task CreatePlatformAsync(CreatePlatformRequestModel PlatformDto);
        Task UpdatePlatformAsync(int id, UpdatePlatformRequestModel PlatformDto);
        Task DeletePlatformAsync(int id);
    }
}
