using AutoMapper;
using Dapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Npgsql;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperGenreRepository : IGenreRepository
    {
        private readonly DatabaseSettings _databaseSettings;
        private IMapper _mapper;

        public DapperGenreRepository(DatabaseSettings databaseSettings, IMapper mapper)
        {
            _databaseSettings = databaseSettings;
            _mapper = mapper;
        }

        public async Task AddAsync(Genre genreEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<Genre>(genreEntity);

                const string sql = @"
                    INSERT INTO ""Genres"" (""Name"", ""Description"", ""Popularity"")
                    VALUES (@Name, @Description, @Popularity)
                    RETURNING ""Id""";

                var id = await connection.ExecuteScalarAsync<int>(sql, entity);
                genreEntity.Id = id;
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

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                const string sql = "SELECT * FROM \"Genres\"";
                var entities = await connection.QueryAsync<GenreEntity>(sql);
                return entities.Select(_mapper.Map<Genre>);
            }
        }

        public async Task<Genre> GetByIdAsync(int id)
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

                return _mapper.Map<Genre>(result);
            }
        }

        public async Task UpdateAsync(Genre genreEntity)
        {
            using (var connection = new NpgsqlConnection(_databaseSettings.ConnectionString))
            {
                var entity = _mapper.Map<GenreEntity>(genreEntity);

                const string sql = @"
                    UPDATE ""Genres"" 
                    SET ""Name"" = @Name, 
                        ""Description"" = @Description,
                        ""Popularity"" = @Popularity
                    WHERE ""Id"" = @Id";

                await connection.ExecuteAsync(sql, entity);
            }
        }
    }
}
