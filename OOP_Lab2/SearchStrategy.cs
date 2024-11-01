using Microsoft.EntityFrameworkCore;
using OOP_Lab2;
using OOP_Lab2.Models;

namespace OOP_Lab2
{
	public interface ISearchStrategy
	{
		Task<List<Artist>> SearchAsync(MusicCatalogContext context, string query);
	}

}

public class SearchByArtist : ISearchStrategy
{
	public async Task<List<Artist>> SearchAsync(MusicCatalogContext context, string query)
	{
		return await context.Artists
			.Where(a => a.Name.Contains(query))
			.ToListAsync();
	}
}

public class SearchByAlbum : ISearchStrategy
{
	public async Task<List<Artist>> SearchAsync(MusicCatalogContext context, string query)
	{
		return await context.Artists
			.Where(a => a.Albums.Any(al => al.Title.Contains(query)))
			.ToListAsync();
	}
}
