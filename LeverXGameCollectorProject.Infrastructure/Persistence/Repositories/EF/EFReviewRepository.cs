using AutoMapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public EFReviewRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Review reviewEntity)
        {
            var entity = _mapper.Map<ReviewEntity>(reviewEntity);
            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
            reviewEntity.Id = entity.Id;
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

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            var entities = await _context.Reviews.AsNoTracking()
            .Include(r => r.Game)
            .ToListAsync();

            return entities.Select(_mapper.Map<Review>);
        }

        public async Task<IEnumerable<Review>> GetByGameAsync(int gameId)
        {
            var entities = await _context.Reviews
            .Where(r => r.Game.Id == gameId)
            .OrderByDescending(r => r.ReviewDate)
            .ToListAsync();

            return entities.Select(_mapper.Map<Review>);
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            var entity = await _context.Reviews
            .Include(r => r.Game)
            .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<Review>(entity);
        }

        public async Task UpdateAsync(Review reviewEntity)
        {
            var entity = _mapper.Map<ReviewEntity>(reviewEntity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
