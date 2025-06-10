using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperGameRepository : IGameRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private const string _getByIdSql = @"
            SELECT g.*, p.*, d.*, f.*
            FROM ""Games"" g
            LEFT JOIN ""Platforms"" p ON g.""PlatformId"" = p.""Id""
            LEFT JOIN ""Developers"" d ON g.""DeveloperId"" = d.""Id""
            LEFT JOIN ""Genres"" f ON g.""GenreId"" = f.""Id""
            WHERE g.""Id"" = @Id";

        private const string _getAllSql = @"
            SELECT g.*, p.*, d.*, f.*
            FROM ""Games"" g
            LEFT JOIN ""Platforms"" p ON g.""PlatformId"" = p.""Id""
            LEFT JOIN ""Developers"" d ON g.""DeveloperId"" = d.""Id""
            LEFT JOIN ""Genres"" f ON g.""GenreId"" = f.""Id""";

        private const string _insertSql = @"
            INSERT INTO ""Games"" (""Title"", ""ReleaseDate"", ""PlatformId"", ""GenreId"", ""DeveloperId"")
            VALUES (@Title, @ReleaseDate, @PlatformId, @GenreId, @DeveloperId)
            RETURNING ""Id""";

        private const string _updateSql = @"
            UPDATE ""Games""
            SET ""Title"" = @Title,
                ""ReleaseDate"" = @ReleaseDate,
                ""PlatformId"" = @PlatformId,
                ""GenreId"" = @GenreId,
                ""DeveloperId"" = @DeveloperId
            WHERE ""Id"" = @Id";

        private const string deleteSql = @"DELETE FROM ""Games"" WHERE ""Id"" = @Id";

        private const string getByPlatformSql = @"SELECT * FROM ""Games"" g WHERE g.""PlatformId"" = @PlatformId";

        public DapperGameRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<GameEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<GameEntity, PlatformEntity, DeveloperEntity, GenreEntity, GameEntity>(
                    _getByIdSql,
                    (game, platform, developer, genre) =>
                    {
                        game.Platform = platform;
                        game.Developer = developer;
                        game.Genre = genre;
                        return game;
                    },
                    new { Id = id },
                    splitOn: "Id,Id"
                );

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<GameEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<GameEntity, PlatformEntity, DeveloperEntity, GenreEntity, GameEntity>(
                    _getAllSql,
                    (game, platform, developer, genre) =>
                    {
                        game.Platform = platform;
                        game.Developer = developer;
                        game.Genre = genre;
                        return game;
                    },
                    splitOn: "Id,Id"
                );

                return result;
            }
        }

        public async Task<int> AddAsync(GameEntity gameEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Title", gameEntity.Title);
                parameters.Add("@ReleaseDate", gameEntity.ReleaseDate);
                parameters.Add("@PlatformId", gameEntity.Platform?.Id);
                parameters.Add("@GenreId", gameEntity.Genre?.Id);
                parameters.Add("@DeveloperId", gameEntity.Developer?.Id);

                var id = await connection.ExecuteScalarAsync<int>(_insertSql, parameters);
                gameEntity.Id = id;
                return id;
            }
        }

        public async Task UpdateAsync(GameEntity gameEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", gameEntity.Id);
                parameters.Add("@Title", gameEntity.Title);
                parameters.Add("@ReleaseDate", gameEntity.ReleaseDate);
                parameters.Add("@PlatformId", gameEntity.Platform?.Id);
                parameters.Add("@GenreId", gameEntity.Genre?.Id);
                parameters.Add("@DeveloperId", gameEntity.Developer?.Id);

                await connection.ExecuteAsync(_updateSql, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(deleteSql, new { Id = id });
            }
        }

        public async Task<IEnumerable<GameEntity>> GetByPlatformAsync(int platformId)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entities = await connection.QueryAsync<GameEntity>(getByPlatformSql, new { PlatformId = platformId });
                return entities;
            }
        }
    }
}
