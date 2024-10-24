namespace OOP_Lab2.Models
{
    public class Track
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; }
        public int Duration { get; set; } // Duration in seconds
        public string AudioFilePath { get; set; }
        public Album Album { get; set; }
    }

}
