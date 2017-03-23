using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicApp2017.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApp2017.Controllers
{
    public class GenresController : Controller
    {
        private MusicDbContext _context;

        public GenresController(MusicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(musicDbContext.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var genreContext = _context.Genres.Include(a => a.Genres);
            var genre = genreContext
                .SingleOrDefault(m => m.GenreID == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("GenreID,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Name"] = genre.Name;
            return View(genre);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = _context.Genres.SingleOrDefault(m => m.GenreID == id);
            if (genre == null)
            {
                return NotFound();
            }
            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Name", genre.Albums);

            return View(genre);
        }
    }
}

