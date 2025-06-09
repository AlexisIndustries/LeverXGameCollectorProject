using System.ComponentModel.DataAnnotations;

namespace LeverXGameCollectorProject.Domain.Persistence.Entities
{
    public class ReviewEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public GameEntity Game { get; set; }
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
