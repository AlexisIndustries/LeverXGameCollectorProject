using AutoMapper;
using Dapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperPlatformRepository : IPlatformRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private IMapper _mapper;

        public DapperPlatformRepository(DatabaseSettings databaseSettings, IMapper mapper)
        {
            _databaseSettings = databaseSettings;
            _mapper = mapper;
        }

        public async Task AddAsync(Platform platformEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<PlatformEntity>(platformEntity);

                const string sql = @"
                    INSERT INTO ""Platforms"" (""Name"", ""Manufacturer"", ""ReleaseYear"")
                    VALUES (@Name, @Manufacturer, @ReleaseYear)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, entity);
                platformEntity.Id = id;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(
                    @"UPDATE ""Games"" SET ""PlatformId"" = NULL WHERE ""PlatformId"" = @Id",
                    new { Id = id });
                const string sql = "DELETE FROM \"Platforms\" WHERE \"Id\" = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "SELECT * FROM \"Platforms\"";
                var entities = await connection.QueryAsync<PlatformEntity>(sql);
                return entities.Select(_mapper.Map<Platform>);
            }
        }

        public async Task<Platform> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                SELECT * FROM ""Platforms""
                WHERE ""Id"" = @Id";

                var result = await connection.QueryFirstOrDefaultAsync<PlatformEntity>(
                    sql,
                    new { Id = id }
                );

                return _mapper.Map<Platform>(result);
            }
        }

        public async Task UpdateAsync(Platform platformEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<PlatformEntity>(platformEntity);

                const string sql = @"
                    UPDATE ""Platforms"" 
                    SET ""Name"" = @Name, 
                        ""Manufacturer"" = @Manufacturer,
                        ""ReleaseYear"" = @ReleaseYear
                    WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, entity);
            }
        }
    }
}
