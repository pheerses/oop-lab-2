using Microsoft.EntityFrameworkCore;
using OOP_Lab2;
using OOP_Lab2.Models;

namespace OOP_Lab2
{
    public interface ISearchStrategy<T>
    {
        Task<List<T>> SearchAsync(MusicCatalogContext context, string query);
    }

}

public class SearchByArtist : ISearchStrategy<Artist>
{
    public async Task<List<Artist>> SearchAsync(MusicCatalogContext context, string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return await context.Artists.ToListAsync();
        }

        query = query.ToLower();
        Console.WriteLine(query);

        return await context.Artists
            .Where(a => a.Name.ToLower().Contains(query))
            .ToListAsync();
    }
}

public class SearchByAlbum : ISearchStrategy<Album>
{
    public async Task<List<Album>> SearchAsync(MusicCatalogContext context, string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return await context.Albums
            .Include(al => al.Artist).ToListAsync();
        }

        query = query.ToLower();


        return await context.Albums
            .Include(al => al.Artist)
            .Where(al => al.Title.ToLower().Contains(query))
            .ToListAsync();
    }
}

public class SearchByTrack : ISearchStrategy<Track>
{
    public async Task<List<Track>> SearchAsync(MusicCatalogContext context, string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return await context.Tracks
            .Include(t => t.Album.Artist).ToListAsync();
        }

        query = query.ToLower();


        return await context.Tracks
            .Include(t => t.Album.Artist)
            .Where(t => t.Title.ToLower().Contains(query))
            .ToListAsync();
    }
}

public class SearchByPlaylist : ISearchStrategy<Playlist>
{
    public async Task<List<Playlist>> SearchAsync(MusicCatalogContext context, string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return await context.Compilations.ToListAsync();
        }

        query = query.ToLower();


        return await context.Compilations
            .Where(p => p.Title.ToLower().Contains(query))
            .ToListAsync();
    }
}
