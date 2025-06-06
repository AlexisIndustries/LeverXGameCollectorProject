using System.ComponentModel.DataAnnotations;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Entities
{
    public class GenreEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Popularity { get; set; }
    }
}
