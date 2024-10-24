
namespace OOP_Lab2.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AvatarPath { get; set; }
        public List<Album> Albums { get; set; }

        public Artist()
        {
            Albums = new List<Album>();
        }
    }
}