using System.ComponentModel.DataAnnotations;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Entities
{
    public class PlatformEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int ReleaseYear { get; set; }
    }
}
