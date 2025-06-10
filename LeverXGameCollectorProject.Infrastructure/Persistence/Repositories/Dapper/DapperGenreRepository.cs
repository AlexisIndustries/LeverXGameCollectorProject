using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperGenreRepository : IGenreRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private const string insertSql = @"
            INSERT INTO ""Genres"" (""Name"", ""Description"", ""Popularity"")
            VALUES (@Name, @Description, @Popularity)
            RETURNING ""Id""";

        private const string updateGamesOnDeleteSql = @"UPDATE ""Games"" SET ""GenreId"" = NULL WHERE ""GenreId"" = @Id";

        private const string deleteSql = @"DELETE FROM ""Genres"" WHERE ""Id"" = @Id";

        private const string selectAllSql = @"SELECT * FROM ""Genres""";

        private const string selectByIdSql = @"
            SELECT * FROM ""Genres""
            WHERE ""Id"" = @Id";

        private const string updateSql = @"
            UPDATE ""Genres""
            SET ""Name"" = @Name,
                ""Description"" = @Description,
                ""Popularity"" = @Popularity
            WHERE ""Id"" = @Id";

        public DapperGenreRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<int> AddAsync(GenreEntity genreEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var id = await connection.ExecuteScalarAsync<int>(insertSql, genreEntity);
                genreEntity.Id = id;
                return id;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(updateGamesOnDeleteSql, new { Id = id });
                await connection.ExecuteAsync(deleteSql, new { Id = id });
            }
        }

        public async Task<IEnumerable<GenreEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entities = await connection.QueryAsync<GenreEntity>(selectAllSql);
                return entities;
            }
        }

        public async Task<GenreEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync<GenreEntity>(
                    selectByIdSql,
                    new { Id = id }
                );

                return result;
            }
        }

        public async Task UpdateAsync(GenreEntity genreEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(updateSql, genreEntity);
            }
        }
    }
}
