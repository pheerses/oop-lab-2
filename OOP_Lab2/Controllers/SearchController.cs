using Microsoft.AspNetCore.Mvc;

namespace OOP_Lab2.Controllers
{
	public class SearchController : Controller
	{
		private readonly MusicCatalogContext _context;
		private ISearchStrategy _searchStrategy;

		public SearchController(MusicCatalogContext context)
		{
			_context = context;
		}

		public void SetStrategy(ISearchStrategy strategy)
		{
			_searchStrategy = strategy;
		}

        public async Task<IActionResult> Search(string query, string searchType)
        {
            switch (searchType)
            {
                case "artist":
                    SetStrategy(new SearchByArtist());
                    break;
                case "album":
                    SetStrategy(new SearchByAlbum());
                    break;
                case "track":
                    SetStrategy(new SearchByTrack());
                    break;
                default:
                    SetStrategy(new SearchByArtist());
                    break;
            }

            var results = await _searchStrategy.SearchAsync(_context, query);
            return View("SearchResults", results);
        }
    }

}