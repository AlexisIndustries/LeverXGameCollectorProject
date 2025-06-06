using Swashbuckle.AspNetCore.Annotations;

namespace LeverXGameCollectorProject.Application.DTOs.Platform
{
    [SwaggerSchema]
    public class PlatformResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public int ReleaseYear { get; set; }
    }
}
