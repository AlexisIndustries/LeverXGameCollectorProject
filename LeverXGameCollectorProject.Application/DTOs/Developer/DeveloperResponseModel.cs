using Swashbuckle.AspNetCore.Annotations;

namespace LeverXGameCollectorProject.Application.DTOs.Developer
{
    [SwaggerSchema]
    public class DeveloperResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
        public DateTime Founded { get; set; }
    }
}
