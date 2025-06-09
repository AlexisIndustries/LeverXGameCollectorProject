using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperGameRepository : IGameRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        public DapperGameRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<GameEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection (_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<GameEntity, PlatformEntity, DeveloperEntity, GenreEntity, GameEntity>(
                    @"SELECT g.*, p.*, d.*, f.*
                      FROM ""Games"" g
                      LEFT JOIN ""Platforms"" p ON g.""PlatformId"" = p.""Id""
                      LEFT JOIN ""Developers"" d ON g.""DeveloperId"" = d.""Id""
                      LEFT JOIN ""Genres"" f ON g.""GenreId"" = f.""Id""
                      WHERE g.""Id"" = @Id",
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
                     @"SELECT g.*, p.*, d.*, f.* 
                      FROM ""Games"" g
                      LEFT JOIN ""Platforms"" p ON g.""PlatformId"" = p.""Id""
                      LEFT JOIN ""Developers"" d ON g.""DeveloperId"" = d.""Id""
                        LEFT JOIN ""Genres"" f ON g.""GenreId"" = f.""Id""",
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
                parameters.Add("@Platform.Id", gameEntity.Platform?.Id);
                parameters.Add("@Genre.Id", gameEntity.Genre?.Id);
                parameters.Add("@Developer.Id", gameEntity.Developer?.Id);

                const string sql = @"
                    INSERT INTO ""Games"" (""Title"", ""ReleaseDate"", ""PlatformId"", ""GenreId"", ""DeveloperId"")
                    VALUES (@Title, @ReleaseDate, @Platform.Id, @Genre.Id, @Developer.Id)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, parameters);
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
                parameters.Add("@Platform.Id", gameEntity.Platform?.Id);
                parameters.Add("@Genre.Id", gameEntity.Genre?.Id);
                parameters.Add("@Developer.Id", gameEntity.Developer?.Id);

                const string sql = @"
                    UPDATE ""Games"" 
                    SET ""Title"" = @Title, 
                        ""ReleaseDate"" = @ReleaseDate,
                        ""PlatformId"" = @Platform.Id,
                        ""GenreId"" = @Genre.Id,
                        ""DeveloperId"" = @Developer.Id
                    WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "DELETE FROM \"Games\" WHERE \"Id\" = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<GameEntity>> GetByPlatformAsync(int platformId)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString)) 
            {
                const string sql = @"SELECT * FROM ""Games"" g WHERE g.""Platform"".""Id"" = @PlatformId";
                var entities = await connection.QueryAsync<GameEntity>(sql);
                return entities;
            }
        }
    }
}
