namespace LeverXGameCollectorProject.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }        
        public DateTime ReleaseDate { get; set; }  
        public string? Description { get; set; }   
        public int PlatformId { get; set; }       
        public int GenreId { get; set; }       
    }
}
