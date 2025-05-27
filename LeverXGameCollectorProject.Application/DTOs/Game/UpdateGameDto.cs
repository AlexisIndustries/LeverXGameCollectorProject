using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.DTOs.Game
{
    public class UpdateGameDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public int[]? PlatformId { get; set; }
        public int[]? GenreId { get; set; }
    }
}
