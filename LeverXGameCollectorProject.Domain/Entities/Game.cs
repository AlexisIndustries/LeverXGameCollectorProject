namespace LeverXGameCollectorProject.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }        
        public DateTime ReleaseDate { get; set; }  
        public string? Description { get; set; }  
        public Developer? Developer { get; set; }
        public Platform? Platform { get; set; }       
        public Genre? Genre { get; set; }       
    }
}
