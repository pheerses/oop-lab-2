using Microsoft.EntityFrameworkCore;
using OOP_Lab2.Models;

namespace OOP_Lab2
{
    public class MusicCatalogContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Compilations { get; set; }

        public MusicCatalogContext(DbContextOptions<MusicCatalogContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=musiccatalog.db");
            }
        }
    }

}
