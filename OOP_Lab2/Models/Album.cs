namespace OOP_Lab2.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; } 
        public string CoverImagePath { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public List<Track> Tracks { get; set; }

        public Album()
        {
            Tracks = new List<Track>();
        }
    }

}
