using System.ComponentModel.DataAnnotations;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Entities
{
    public class DeveloperEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public DateTime Founded { get; set; }
    }
}
