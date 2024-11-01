using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_Lab2.Builders;
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
        public async Task<IActionResult> Create(string name, IFormFile avatar)
        {
            var artist = new Artist { Name = name };

            if (avatar != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(avatar.FileName);
                var filePath = Path.Combine("wwwroot/avatars", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(stream);
                }

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
			var genre = await _context.Genres.FindAsync(genreId);

			if (artist == null || genre == null)
			{
				return NotFound();
			}

			var albumBuilder = new AlbumBuilder();
			var album = albumBuilder
				.StartNew(title)
				.SetArtist(artist)
				.SetGenre(genre)
				.SetReleaseYear(releaseYear)
				.Build();

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
