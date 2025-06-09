using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperPlatformRepository : IPlatformRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        public DapperPlatformRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<int> AddAsync(PlatformEntity platformEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                    INSERT INTO ""Platforms"" (""Name"", ""Manufacturer"", ""ReleaseYear"")
                    VALUES (@Name, @Manufacturer, @ReleaseYear)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, platformEntity);
                platformEntity.Id = id;
                return id;
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

        public async Task<IEnumerable<PlatformEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "SELECT * FROM \"Platforms\"";
                var entities = await connection.QueryAsync<PlatformEntity>(sql);
                return entities;
            }
        }

        public async Task<PlatformEntity> GetByIdAsync(int id)
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

                return result;
            }
        }

        public async Task UpdateAsync(PlatformEntity platformEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                    UPDATE ""Platforms"" 
                    SET ""Name"" = @Name, 
                        ""Manufacturer"" = @Manufacturer,
                        ""ReleaseYear"" = @ReleaseYear
                    WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, platformEntity);
            }
        }
    }
}
