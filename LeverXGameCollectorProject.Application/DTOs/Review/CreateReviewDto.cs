using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.DTOs.Review
{
    public class CreateReviewDto
    {
        public int GameId { get; set; }
        public string? ReviewerName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
