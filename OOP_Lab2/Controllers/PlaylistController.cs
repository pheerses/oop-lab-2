using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_Lab2.Models;

namespace OOP_Lab2.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly MusicCatalogContext _context;

        public PlaylistController(MusicCatalogContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, List<int> SelectedTrackIds)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "Title is required");
                ViewBag.Tracks = await _context.Tracks.Include(t => t.Album).ToListAsync();
                return View();
            }

            var playlist = new Playlist { Title = title };

            foreach (var trackId in SelectedTrackIds)
            {
                var track = await _context.Tracks.FindAsync(trackId);
                if (track != null)
                {
                    playlist.Tracks.Add(track);
                }
            }

            _context.Compilations.Add(playlist);
            await _context.SaveChangesAsync();

            return RedirectToAction("SearchPlaylists");
        }
    }
}
