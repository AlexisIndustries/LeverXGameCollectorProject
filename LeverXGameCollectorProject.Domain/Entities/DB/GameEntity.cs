using System.ComponentModel.DataAnnotations;

namespace LeverXGameCollectorProject.Domain.Persistence.Entities
{
    public class GameEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        [Required]
        public DeveloperEntity Developer { get; set; }
        [Required]
        public PlatformEntity Platform { get; set; }
        [Required]
        public GenreEntity Genre { get; set; }
    }
}
