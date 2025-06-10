using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperReviewRepository : IReviewRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        private const string _getByIdSql = @"
            SELECT r.*, g.*
            FROM ""Reviews"" r
            LEFT JOIN ""Games"" g ON r.""GameId"" = g.""Id""
            WHERE r.""Id"" = @Id";

        private const string _getByGameSql = @"
            SELECT r.*, g.*
            FROM ""Reviews"" r
            LEFT JOIN ""Games"" g ON r.""GameId"" = g.""Id""
            WHERE r.""GameId"" = @Id";

        private const string _insertSql = @"
            INSERT INTO ""Reviews"" (""GameId"", ""ReviewerName"", ""Rating"", ""Comment"", ""ReviewDate"")
            VALUES (@GameId, @ReviewerName, @Rating, @Comment, @ReviewDate)
            RETURNING ""Id""";

        private const string _updateSql = @"
            UPDATE ""Reviews""
            SET ""Comment"" = @Comment,
                ""Rating"" = @Rating
            WHERE ""Id"" = @Id";

        private const string _deleteSql = @"DELETE FROM ""Reviews"" WHERE ""Id"" = @Id";

        private const string _getAllSql = @"
            SELECT r.*, g.*
            FROM ""Reviews"" r
            LEFT JOIN ""Games"" g ON r.""GameId"" = g.""Id""";

        public DapperReviewRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<ReviewEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = await connection.QueryAsync<ReviewEntity, GameEntity, ReviewEntity>(
                    _getByIdSql,
                    (review, game) =>
                    {
                        review.Game = game;
                        return review;
                    },
                    new { Id = id },
                    splitOn: "Id,Id"
                );
                return entity.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<ReviewEntity>> GetByGameAsync(int gameId)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entities = await connection.QueryAsync<ReviewEntity, GameEntity, ReviewEntity>(
                    _getByGameSql,
                    (review, game) =>
                    {
                        review.Game = game;
                        return review;
                    },
                    new { Id = gameId },
                    splitOn: "Id,Id"
                );
                return entities;
            }
        }

        public async Task<int> AddAsync(ReviewEntity entity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GameId", entity.Game.Id);
                parameters.Add("@ReviewerName", entity.ReviewerName);
                parameters.Add("@Rating", entity.Rating);
                parameters.Add("@Comment", entity.Comment);
                parameters.Add("@ReviewDate", entity.ReviewDate);

                var id = await connection.ExecuteScalarAsync<int>(_insertSql, parameters);
                entity.Id = id;
                return id;
            }
        }

        public async Task UpdateAsync(ReviewEntity review)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(_updateSql, review);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(_deleteSql, new { Id = id });
            }
        }

        public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entities = await connection.QueryAsync<ReviewEntity, GameEntity, ReviewEntity>(
                    _getAllSql,
                    (review, game) =>
                    {
                        review.Game = game;
                        return review;
                    },
                    splitOn: "Id,Id"
                );
                return entities;
            }
        }
    }
}