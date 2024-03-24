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
        public IActionResult Index(string searchString, string minPrice, string maxPrice)
        {
            var searchBooks = _context.Bookss.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchBooks = searchBooks.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(minPrice))
            {
                var min = int.Parse(minPrice);
                    searchBooks = searchBooks.Where(b => b.Price >= min);
                }
            
            if (!string.IsNullOrEmpty(maxPrice))
            {
                var max = int.Parse(maxPrice);

                searchBooks = searchBooks.Where(b => b.Price <= max);
                
            }
            var books = searchBooks.Include(b => b.Categories).ToList();

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
