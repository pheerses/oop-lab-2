namespace OOP_Lab2.Models
{
    public class Playlist
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; }
        public List<Track> Tracks { get; set; }

        public Playlist()
        {
            Tracks = new List<Track>();
        }
    }

}
