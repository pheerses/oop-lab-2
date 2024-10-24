using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_Lab2.Models;


namespace OOP_Lab2.Controllers
{
    public class ArtistController : Controller
    {
        private readonly MusicCatalogContext _context;

        public ArtistController(MusicCatalogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Artists.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(string name, IFormFile avatar)
        {
            var artist = new Artist { Name = name };

            if (avatar != null)
            {
                // Генерируем уникальное имя для файла аватарки
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(avatar.FileName);
                var filePath = Path.Combine("wwwroot/avatars", uniqueFileName);

                // Сохраняем файл на сервере
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(stream);
                }

                // Сохраняем путь к аватарке в базу данных
                artist.AvatarPath = "/avatars/" + uniqueFileName;
            }

            _context.Add(artist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int artistId)
        {

            var artist = await _context.Artists.FindAsync(artistId);
            if (artist != null)
            {
                _context.Remove(artist);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {

            ViewBag.Genres = _context.Genres.ToList();
            var artist = await _context.Artists
                .Include(a => a.Albums)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(string title, int artistId, int releaseYear, int genreId, IFormFile coverImage)
        {
            var artist = await _context.Artists.FindAsync(artistId);
            Console.WriteLine(title, artistId, genreId);
            if (artist == null)
            {
                return NotFound();
            }

            var album = new Album
            {
                Title = title,
                ReleaseYear = releaseYear,
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

            return RedirectToAction("Details", new { id = artistId });
        }

    }

}
