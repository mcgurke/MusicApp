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
            var musicDbContext = _context.Genres;
            return View(musicDbContext.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["GenreID"] = id;
            ViewData["GenreName"] = (_context.Genres.SingleOrDefault(m => m.GenreID == id)).Name;
            var musicDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(musicDbContext.ToList());
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

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("GenreID, Name")] Genre genre)
        {
            if (id != genre.GenreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                   _context.Update(genre);
                   _context.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(genre);
        }

    }
}

