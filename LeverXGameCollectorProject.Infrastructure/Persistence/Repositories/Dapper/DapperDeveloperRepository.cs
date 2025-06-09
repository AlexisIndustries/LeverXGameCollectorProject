using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperDeveloperRepository : IDeveloperRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private const string getByIdSql = @"
                SELECT * FROM ""Developers""
                WHERE ""Id"" = @Id""";
        private const string selectsql = "SELECT * FROM \"Developers\"";
        const string insertsql = @"
                    INSERT INTO ""Developers"" (""Name"", ""Country"", ""Website"", ""Founded"")
                    VALUES (@Name, @Country, @Website, @Founded)
                    RETURNING ""Id""";
        const string updatesql = @"
                    UPDATE ""Developers"" 
                    SET ""Name"" = @Name, 
                        ""Country"" = @Country,
                        ""Website"" = @Website,
                        ""Founded"" = @Founded
                    WHERE ""Id"" = @Id";
        const string delUpdatesql = @"UPDATE ""Games"" SET ""DeveloperId"" = NULL WHERE ""DeveloperId"" = @Id";
        const string deletesql = "DELETE FROM \"Developers\" WHERE \"Id\" = @Id";

        public DapperDeveloperRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<DeveloperEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync<DeveloperEntity>(
                    getByIdSql,
                    new { Id = id }
                );

                return result;
            }
        }

        public async Task<IEnumerable<DeveloperEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {

                var entities = await connection.QueryAsync<DeveloperEntity>(selectsql);
                return entities;
            }
        }

        public async Task<int> AddAsync(DeveloperEntity developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var id = await connection.ExecuteScalarAsync<int>(insertsql, developerEntity);
                developerEntity.Id = id;
                return id;
            }
        }

        public async Task UpdateAsync(DeveloperEntity developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(updatesql, developerEntity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(
                    delUpdatesql,
                    new { Id = id });
                await connection.ExecuteAsync(deletesql, new { Id = id });
            }
        }
    }
}
