using EProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EProject.Controllers
{
    [AllowAnonymous]
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var books = _context.Bookss.Include("Categories");
            return View(books);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the book by its ID
            var book = _context.Bookss.Find(id);

            // If the book doesn't exist, return NotFound
            if (book == null)
            {
                return NotFound();
            }

            return View(book);




        }
    }
}
