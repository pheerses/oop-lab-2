using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_Lab2.Models;

namespace OOP_Lab2.Controllers
{

    public class TrackController : Controller
    {
        private readonly MusicCatalogContext _context;

        public TrackController(MusicCatalogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tracks = await _context.Tracks.Include(t => t.Album.Artist).ToListAsync();
            return View(tracks);
        }

        public IActionResult Create()
        {
            ViewBag.Albums = _context.Albums.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, int duration, int albumId, IFormFile audioFile)
        {
            var track = new Track
            {
                Title = title,
                Duration = duration,
                Album = await _context.Albums.FindAsync(albumId)
            };

            if (audioFile != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(audioFile.FileName);
                var filePath = Path.Combine("wwwroot/audio", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(stream);
                }

                track.AudioFilePath = "/audio/" + uniqueFileName;
            }

            _context.Add(track);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {

            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult View(int id)
        {
            ViewBag.Track = _context.Tracks.Find(id);
            return View();
        }
    }

}
