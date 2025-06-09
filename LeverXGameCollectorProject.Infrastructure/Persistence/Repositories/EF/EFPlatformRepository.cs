using AutoMapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFPlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public EFPlatformRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Platform platformEntity)
        {
            var entity = _mapper.Map<PlatformEntity>(platformEntity);
            await _context.Platforms.AddAsync(entity);
            await _context.SaveChangesAsync();
            platformEntity.Id = entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Platforms.FindAsync(id);
            if (entity != null)
            {
                _context.Platforms.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            var entities = await _context.Platforms.AsNoTracking()
            .ToListAsync();

            return entities.Select(_mapper.Map<Platform>);
        }

        public async Task<Platform> GetByIdAsync(int id)
        {
            var entity = await _context.Platforms.AsNoTracking()
           .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<Platform>(entity);
        }

        public async Task UpdateAsync(Platform platformEntity)
        {
            var entity = _mapper.Map<PlatformEntity>(platformEntity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
