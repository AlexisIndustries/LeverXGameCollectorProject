using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFGameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public EFGameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(GameEntity gameEntity)
        {
            gameEntity.Developer = await _context.Developers.FindAsync(gameEntity.Developer.Id);
            gameEntity.Platform = await _context.Platforms.FindAsync(gameEntity.Platform.Id);
            gameEntity.Genre = await _context.Genres.FindAsync(gameEntity.Genre.Id);
            var ent = await _context.Games.AddAsync(gameEntity);
            await _context.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Games.FindAsync(id);
            if (entity != null)
            {
                _context.Games.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GameEntity>> GetAllAsync()
        {
            var entities = await _context.Games.AsNoTracking()
            .Include(g => g.Platform)
            .Include(g => g.Developer)
            .Include(g => g.Genre)
            .ToListAsync();

            return entities;
        }

        public async Task<GameEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Games.AsNoTracking()
                .Include(g => g.Platform)
                .Include(g => g.Developer)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<IEnumerable<GameEntity>> GetByPlatformAsync(int platformId)
        {
            var entities = await _context.Games.AsNoTracking()
            .Where(g => g.Platform.Id == platformId)
            .Include(g => g.Platform)
            .ToListAsync();

            return entities;
        }

        public async Task UpdateAsync(GameEntity gameEntity)
        {
            _context.Entry(gameEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
