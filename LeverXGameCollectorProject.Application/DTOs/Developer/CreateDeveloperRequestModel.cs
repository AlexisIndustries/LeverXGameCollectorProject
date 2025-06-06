namespace LeverXGameCollectorProject.Application.DTOs.Developer
{
    public class CreateDeveloperRequestModel
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
        public DateTime Founded { get; set; }
    }
}
