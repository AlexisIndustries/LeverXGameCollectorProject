using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperPlatformRepository : IPlatformRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private const string _insertSql = @"
            INSERT INTO ""Platforms"" (""Name"", ""Manufacturer"", ""ReleaseYear"")
            VALUES (@Name, @Manufacturer, @ReleaseYear)
            RETURNING ""Id""";

        private const string _updateGamesOnDeleteSql = @"UPDATE ""Games"" SET ""PlatformId"" = NULL WHERE ""PlatformId"" = @Id";

        private const string _deleteSql = @"DELETE FROM ""Platforms"" WHERE ""Id"" = @Id";

        private const string _selectAllSql = @"SELECT * FROM ""Platforms""";

        private const string _selectByIdSql = @"
            SELECT * FROM ""Platforms""
            WHERE ""Id"" = @Id";

        private const string _updateSql = @"
            UPDATE ""Platforms""
            SET ""Name"" = @Name,
                ""Manufacturer"" = @Manufacturer,
                ""ReleaseYear"" = @ReleaseYear
            WHERE ""Id"" = @Id";

        public DapperPlatformRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<int> AddAsync(PlatformEntity platformEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var id = await connection.ExecuteScalarAsync<int>(_insertSql, platformEntity);
                platformEntity.Id = id;
                return id;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(_updateGamesOnDeleteSql, new { Id = id });
                await connection.ExecuteAsync(_deleteSql, new { Id = id });
            }
        }

        public async Task<IEnumerable<PlatformEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entities = await connection.QueryAsync<PlatformEntity>(_selectAllSql);
                return entities;
            }
        }

        public async Task<PlatformEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync<PlatformEntity>(
                    _selectByIdSql,
                    new { Id = id }
                );

                return result;
            }
        }

        public async Task UpdateAsync(PlatformEntity platformEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(_updateSql, platformEntity);
            }
        }
    }
}
