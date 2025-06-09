using AutoMapper;
using Dapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperDeveloperRepository : IDeveloperRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private IMapper _mapper;

        public DapperDeveloperRepository(DatabaseSettings databaseSettings, IMapper mapper)
        {
            _databaseSettings = databaseSettings;
            _mapper = mapper;
        }

        public async Task<Developer> GetByIdAsync(int id)
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

                return _mapper.Map<Developer>(result);
            }
        }

        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "SELECT * FROM \"Developers\"";
                var entities = await connection.QueryAsync<DeveloperEntity>(sql);
                return entities.Select(_mapper.Map<Developer>);
            }
        }

        public async Task AddAsync(Developer developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<Developer>(developerEntity);

                const string sql = @"
                    INSERT INTO ""Developers"" (""Name"", ""Country"", ""Website"", ""Founded"")
                    VALUES (@Name, @Country, @Website, @Founded)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, entity);
                developerEntity.Id = id;
            }
        }

        public async Task UpdateAsync(Developer developerEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<Developer>(developerEntity);

                const string sql = @"
                    UPDATE ""Developers"" 
                    SET ""Name"" = @Name, 
                        ""Country"" = @Country,
                        ""Website"" = @Website,
                        ""Founded"" = @Founded
                    WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, entity);
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
