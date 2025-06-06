using AutoMapper;
using Dapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperReviewRepository : IReviewRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private IMapper _mapper;

        public DapperReviewRepository(DatabaseSettings databaseSettings, IMapper mapper)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = await connection.QueryAsync<ReviewEntity, GameEntity, Review>(
                    @"SELECT r.*, g.*
                      FROM ""Reviews"" r
                      LEFT JOIN ""Games"" g ON ""GameId"" = g.""Id""
                      WHERE r.""Id"" = @Id",
                    (review, game) =>
                    {
                        review.Game = game;
                        return _mapper.Map<Review>(review);
                    },
                    new { Id = id },
                    splitOn: "Id,Id"
                );
                return entity.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Review>> GetByGameAsync(int gameId)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entities = await connection.QueryAsync<ReviewEntity, GameEntity, Review>(
                    @"SELECT r.*, g.*
                      FROM ""Reviews"" r
                      LEFT JOIN ""Games"" g ON ""GameId"" = g.""Id""
                      WHERE r.""GameId"" = @Id",
                    (review, game) =>
                    {
                        review.Game = game;
                        return _mapper.Map<Review>(review);
                    },
                    new { Id = gameId },
                    splitOn: "Id,Id"
                );
                return entities.Select(_mapper.Map<Review>);
            }
        }

        public async Task AddAsync(Review review)
        {
            var entity = _mapper.Map<ReviewEntity>(review);

            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString)) {
                var parameters = new DynamicParameters();
                parameters.Add("@GameId", entity.Game.Id);
                parameters.Add("@ReviewerName", entity.ReviewerName);
                parameters.Add("@Rating", entity.Rating);
                parameters.Add("@Comment", entity.Comment);
                parameters.Add("@ReviewDate", entity.ReviewDate);
                const string sql = @"
                    INSERT INTO ""Reviews"" (""GameId"", ""ReviewerName"", ""Rating"", ""Comment"", ""ReviewDate"")
                    VALUES (@GameId, @ReviewerName, @Rating, @Comment, @ReviewDate)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, parameters);
                review.Id = id;
            }
        }

        public async Task UpdateAsync(Review review)
        {
            var entity = _mapper.Map<ReviewEntity>(review);

            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                UPDATE ""Reviews"" 
                SET ""Comment"" = @Comment, 
                    ""Rating"" = @Rating
                WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "DELETE FROM \"Reviews\" WHERE \"Id\" = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entities = await connection.QueryAsync<ReviewEntity, GameEntity, Review>(
                    @"SELECT r.*, g.*
                      FROM ""Reviews"" r
                      LEFT JOIN ""Games"" g ON ""GameId"" = g.""Id""",
                    (review, game) =>
                    {
                        review.Game = game;
                        return _mapper.Map<Review>(review);
                    },
                    splitOn: "Id,Id"
                );
                return entities.Select(_mapper.Map<Review>);
            }
        }
    }
}
