using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFDeveloperRepository : IDeveloperRepository
    {
        private readonly ApplicationDbContext _context;

        public EFDeveloperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(DeveloperEntity developerEntity)
        {
            var ent = await _context.Developers.AddAsync(developerEntity);
            await _context.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Developers.FindAsync(id);
            if (entity != null)
            {
                _context.Developers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DeveloperEntity>> GetAllAsync()
        {
            var entities = await _context.Developers.AsNoTracking()
            .ToListAsync();

            return entities;
        }

        public async Task<DeveloperEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Developers.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

            return entity;
        }

        public async Task UpdateAsync(DeveloperEntity developerEntity)
        {
            _context.Entry(developerEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
