using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOP_Lab2.Models;

namespace OOP_Lab2.Controllers
{

    public class GenreController : Controller
    {
        private readonly MusicCatalogContext _context;

        public GenreController(MusicCatalogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var genre = new Genre
            {
                Name = name
            };

            _context.Add(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int genreId)
        {

            var genre = await _context.Genres.FindAsync(genreId);
            if (genre != null)
            {
                _context.Remove(genre);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult View(int genreId)
        {
            ViewBag.Genre = _context.Genres.Find(genreId);
            return View();
        }
    }

}
