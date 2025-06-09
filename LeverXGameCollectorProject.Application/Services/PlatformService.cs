using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;

namespace LeverXGameCollectorProject.Application.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformService(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlatformResponseModel>> GetAllPlatformsAsync()
        {
            var platforms = await _platformRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlatformResponseModel>>(platforms);
        }

        public async Task<PlatformResponseModel> GetPlatformByIdAsync(int id)
        {
            var platform = await _platformRepository.GetByIdAsync(id);
            return _mapper.Map<PlatformResponseModel>(platform);
        }

        public async Task<int> CreatePlatformAsync(CreatePlatformRequestModel platformDto)
        {
            var platform = _mapper.Map<PlatformEntity>(platformDto);
            var id = await _platformRepository.AddAsync(platform);
            return id;
        }

        public async Task UpdatePlatformAsync(int id, UpdatePlatformRequestModel platformDto)
        {
            var platform = await _platformRepository.GetByIdAsync(id);
            _mapper.Map(platformDto, platform);
            await _platformRepository.UpdateAsync(platform);
        }

        public async Task DeletePlatformAsync(int id)
        {
            await _platformRepository.DeleteAsync(id);
        }
    }
}
