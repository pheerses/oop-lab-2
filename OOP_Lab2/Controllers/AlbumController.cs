using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_Lab2.Models;

namespace OOP_Lab2.Controllers
{

    public class AlbumController : Controller
    {
        private readonly MusicCatalogContext _context;

        public AlbumController(MusicCatalogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _context.Albums.Include(a => a.Artist).ToListAsync();
            return View(albums);
        }

        public IActionResult Create()
        {
            ViewBag.Artists = _context.Artists.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, int artistId, int genreId, IFormFile coverImage)
        {
            var album = new Album
            {
                Title = title,
                Artist = await _context.Artists.FindAsync(artistId),
                Genre = await _context.Genres.FindAsync(genreId)
            };

            if (coverImage != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(coverImage.FileName);
                var filePath = Path.Combine("wwwroot/covers", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                album.CoverImagePath = "/covers/" + uniqueFileName;
            }


            _context.Add(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int albumId)
        {

            var album = await _context.Albums.FindAsync(albumId);
            if (album != null)
            {
                _context.Remove(album);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Tracks)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrack(string title, int duration, int albumId, IFormFile audioFile)
        {
            var album = await _context.Albums.FindAsync(albumId);
            if (album == null)
            {
                return NotFound();
            }

            var track = new Track
            {
                Title = title,
                Duration = duration,
                Album = album
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

            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = albumId });
        }


    }

}
