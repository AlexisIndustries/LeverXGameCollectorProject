namespace LeverXGameCollectorProject.Application.DTOs.Review
{
    public class UpdateReviewRequestModel
    {
        public int GameId { get; set; }
        public string? ReviewerName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
