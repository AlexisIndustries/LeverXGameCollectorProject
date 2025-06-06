using AutoMapper;
using Dapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperGameRepository : IGameRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private IMapper _mapper;

        public DapperGameRepository(DatabaseSettings databaseSettings, IMapper mapper)
        {
            _databaseSettings = databaseSettings;
            _mapper = mapper;
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection (_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<GameEntity, PlatformEntity, DeveloperEntity, GenreEntity, Game>(
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
                        return _mapper.Map<Game>(game);
                    },
                    new { Id = id },
                    splitOn: "Id,Id"
                );

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryAsync<GameEntity, PlatformEntity, DeveloperEntity, GenreEntity, Game>(
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
                         return _mapper.Map<Game>(game);
                     },
                     splitOn: "Id,Id"
                 );

                return result.Select(_mapper.Map<Game>);
            }
        }

        public async Task AddAsync(Game gameEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<GameEntity>(gameEntity);

                var parameters = new DynamicParameters();
                parameters.Add("@Title", entity.Title);
                parameters.Add("@ReleaseDate", entity.ReleaseDate);
                parameters.Add("@Platform.Id", entity.Platform?.Id);
                parameters.Add("@Genre.Id", entity.Genre?.Id);
                parameters.Add("@Developer.Id", entity.Developer?.Id);

                const string sql = @"
                    INSERT INTO ""Games"" (""Title"", ""ReleaseDate"", ""PlatformId"", ""GenreId"", ""DeveloperId"")
                    VALUES (@Title, @ReleaseDate, @Platform.Id, @Genre.Id, @Developer.Id)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, parameters);
                gameEntity.Id = id;
            }
        }

        public async Task UpdateAsync(Game gameEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<GameEntity>(gameEntity);

                var parameters = new DynamicParameters();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@Title", entity.Title);
                parameters.Add("@ReleaseDate", entity.ReleaseDate);
                parameters.Add("@Platform.Id", entity.Platform?.Id);
                parameters.Add("@Genre.Id", entity.Genre?.Id);
                parameters.Add("@Developer.Id", entity.Developer?.Id);

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

        public async Task<IEnumerable<Game>> GetByPlatformAsync(int platformId)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString)) 
            {
                const string sql = @"SELECT * FROM ""Games"" g WHERE g.""Platform"".""Id"" = @PlatformId";
                var entities = await connection.QueryAsync<GameEntity>(sql);
                return entities.Select(_mapper.Map<Game>);
            }
        }
    }
}
