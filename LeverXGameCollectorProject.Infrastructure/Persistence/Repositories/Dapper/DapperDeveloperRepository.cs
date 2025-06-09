using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperDeveloperRepository : IDeveloperRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        public DapperDeveloperRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<DeveloperEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                SELECT * FROM ""Developers""
                WHERE ""Id"" = @Id";

                var result = await connection.QueryFirstOrDefaultAsync<DeveloperEntity>(
                    sql,
                    new { Id = id }
                );

                return result;
            }
        }

        public async Task<IEnumerable<DeveloperEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "SELECT * FROM \"Developers\"";
                var entities = await connection.QueryAsync<DeveloperEntity>(sql);
                return entities;
            }
        }

        public async Task<int> AddAsync(DeveloperEntity developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                    INSERT INTO ""Developers"" (""Name"", ""Country"", ""Website"", ""Founded"")
                    VALUES (@Name, @Country, @Website, @Founded)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, developerEntity);
                developerEntity.Id = id;
                return id;
            }
        }

        public async Task UpdateAsync(DeveloperEntity developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                    UPDATE ""Developers"" 
                    SET ""Name"" = @Name, 
                        ""Country"" = @Country,
                        ""Website"" = @Website,
                        ""Founded"" = @Founded
                    WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, developerEntity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(
                    @"UPDATE ""Games"" SET ""DeveloperId"" = NULL WHERE ""DeveloperId"" = @Id",
                    new { Id = id });
                const string sql = "DELETE FROM \"Developers\" WHERE \"Id\" = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
