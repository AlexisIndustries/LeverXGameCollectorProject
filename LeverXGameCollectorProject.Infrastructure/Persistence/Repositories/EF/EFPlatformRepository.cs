using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFPlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _context;

        public EFPlatformRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(PlatformEntity platformEntity)
        {
            await _context.Platforms.AddAsync(platformEntity);
            var id = await _context.SaveChangesAsync();
            platformEntity.Id = id;
            return id;
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

        public async Task<IEnumerable<PlatformEntity>> GetAllAsync()
        {
            var entities = await _context.Platforms.AsNoTracking()
            .ToListAsync();

            return entities;
        }

        public async Task<PlatformEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Platforms.AsNoTracking()
           .FirstOrDefaultAsync(r => r.Id == id);

            return entity;
        }

        public async Task UpdateAsync(PlatformEntity platformEntity)
        {
            _context.Entry(platformEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
