using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public EFReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(ReviewEntity reviewEntity)
        {
            var ent = await _context.Reviews.AddAsync(reviewEntity);
            await _context.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Reviews.FindAsync(id);
            if (entity != null)
            {
                _context.Reviews.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
        {
            var entities = await _context.Reviews.AsNoTracking()
            .Include(r => r.Game)
            .ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<ReviewEntity>> GetByGameAsync(int gameId)
        {
            var entities = await _context.Reviews
            .Where(r => r.Game.Id == gameId)
            .OrderByDescending(r => r.ReviewDate)
            .ToListAsync();

            return entities;
        }

        public async Task<ReviewEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Reviews
            .Include(r => r.Game)
            .FirstOrDefaultAsync(r => r.Id == id);

            return entity;
        }

        public async Task UpdateAsync(ReviewEntity reviewEntity)
        {
            _context.Entry(reviewEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
