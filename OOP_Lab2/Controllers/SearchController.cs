using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OOP_Lab2.Controllers
{
    public class SearchController : Controller
    {
        private readonly MusicCatalogContext _context;
        private ISearchStrategy<object> _searchStrategy;

        public SearchController(MusicCatalogContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> SearchArtists(string query)
        {
            return await Search(query, new SearchByArtist());
        }

        public async Task<IActionResult> SearchAlbums(string query)
        {
            return await Search(query, new SearchByAlbum());
        }

        public async Task<IActionResult> SearchTracks(string query)
        {
            return await Search(query, new SearchByTrack());
        }

        public async Task<IActionResult> SearchPlaylists(string query)
        {
            return await Search(query, new SearchByPlaylist());
        }



        public async Task<IActionResult> Search<T>(string query, ISearchStrategy<T> strategy) where T : class
        {
            var results = await strategy.SearchAsync(_context, query);
            ViewBag.Tracks = _context.Tracks
                .Include(t => t.Album).ToList();
            return View("SearchResults", results);
        }

    }


}