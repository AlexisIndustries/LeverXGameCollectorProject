using Swashbuckle.AspNetCore.Annotations;

namespace LeverXGameCollectorProject.Application.DTOs.Genre
{
    [SwaggerSchema]
    public class GenreResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Popularity { get; set; }
    }
}
