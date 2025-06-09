using Dapper;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperGenreRepository : IGenreRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        public DapperGenreRepository(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public async Task<int> AddAsync(GenreEntity genreEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                    INSERT INTO ""Genres"" (""Name"", ""Description"", ""Popularity"")
                    VALUES (@Name, @Description, @Popularity)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, genreEntity);
                genreEntity.Id = id;
                return id;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                await connection.ExecuteAsync(
                    @"UPDATE ""Games"" SET ""GenreId"" = NULL WHERE ""GenreId"" = @Id",
                    new { Id = id });
                const string sql = "DELETE FROM \"Genres\" WHERE \"Id\" = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<GenreEntity>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "SELECT * FROM \"Genres\"";
                var entities = await connection.QueryAsync<GenreEntity>(sql);
                return entities;
            }
        }

        public async Task<GenreEntity> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                SELECT * FROM ""Genres""
                WHERE ""Id"" = @Id";

                var result = await connection.QueryFirstOrDefaultAsync<GenreEntity>(
                    sql,
                    new { Id = id }
                );

                return result;
            }
        }

        public async Task UpdateAsync(GenreEntity genreEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = @"
                    UPDATE ""Genres"" 
                    SET ""Name"" = @Name, 
                        ""Description"" = @Description,
                        ""Popularity"" = @Popularity
                    WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, genreEntity);
            }
        }
    }
}
