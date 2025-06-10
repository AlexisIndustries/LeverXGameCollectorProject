using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperDeveloperRepository : IDeveloperRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private const string _getByIdSql = @"
                SELECT * FROM ""Developers""
                WHERE ""Id"" = @Id""";
        private const string _selectSql = "SELECT * FROM \"Developers\"";
        private const string _insertSql = @"
                    INSERT INTO ""Developers"" (""Name"", ""Country"", ""Website"", ""Founded"")
                    VALUES (@Name, @Country, @Website, @Founded)
                    RETURNING ""Id""";
        private const string _updateSql = @"
                    UPDATE ""Developers"" 
                    SET ""Name"" = @Name, 
                        ""Country"" = @Country,
                        ""Website"" = @Website,
                        ""Founded"" = @Founded
                    WHERE ""Id"" = @Id";
        private const string _delUpdateSql = @"UPDATE ""Games"" SET ""DeveloperId"" = NULL WHERE ""DeveloperId"" = @Id";
        private const string _deleteSql = "DELETE FROM \"Developers\" WHERE \"Id\" = @Id";

        public DapperDeveloperRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<DeveloperEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync<DeveloperEntity>(
                    _getByIdSql,
                    new { Id = id }
                );

                return result;
            }
        }

        public async Task<IEnumerable<DeveloperEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {

                var entities = await connection.QueryAsync<DeveloperEntity>(_selectSql);
                return entities;
            }
        }

        public async Task<int> AddAsync(DeveloperEntity developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var id = await connection.ExecuteScalarAsync<int>(_insertSql, developerEntity);
                developerEntity.Id = id;
                return id;
            }
        }

        public async Task UpdateAsync(DeveloperEntity developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(_updateSql, developerEntity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(
                    _delUpdateSql,
                    new { Id = id });
                await connection.ExecuteAsync(_deleteSql, new { Id = id });
            }
        }
    }
}
