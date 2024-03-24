using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public BooksController(ApplicationDbContext context,IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
            
        }
        
        public IActionResult Index()
        {
            var products = _context.Bookss.Include("Categories");
            return View(products);
        }

        [HttpGet]

        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        private void LoadCategories()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CId", "CName");
        }

        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            string filename = " ";
            if (model.photo != null)
            {
                string uploadfolder = Path.Combine(_host.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                model.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            // Find the selected category by its ID
            var category = _context.Categories.Find(model.CategoryId);

            // Create a new Books object and assign properties
            Books b1 = new Books
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = filename,
                ISBN = model.ISBN,
                DatePublished = model.DatePublished,
                Language = model.Language,
                Price = model.Price,
                Publication = model.Publication,
                CategoryId = model.CategoryId, // Assign the CategoryId
                Categories = category // Assign the Category object
            };

            _context.Bookss.Add(b1);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                NotFound();
            }
            LoadCategories();
            var product = _context.Bookss.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(BookViewModel model)
        {
            // Find the existing book in the database
            var existingBook = _context.Bookss.Find(model.Id);

            // Retain the existing image URL if no new image is uploaded
            string filename = existingBook.ImageUrl;

            if (model.photo != null)
            {
                // Handle file upload
                string uploadfolder = Path.Combine(_host.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                model.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            // Find the selected category by its ID
            var category = _context.Categories.Find(model.CategoryId);

            // Update the properties of the existing Books object
            existingBook.Title = model.Title;
            existingBook.Author = model.Author;
            existingBook.Description = model.Description;
            existingBook.ImageUrl = filename; // Update or retain the image URL
            existingBook.ISBN = model.ISBN;
            existingBook.DatePublished = model.DatePublished;
            existingBook.Language = model.Language;
            existingBook.Price = model.Price;
            existingBook.Publication = model.Publication;
            existingBook.CategoryId = model.CategoryId; // Assign the CategoryId
            existingBook.Categories = category; // Assign the Category object

            _context.Bookss.Update(existingBook);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Load categories for the view
            LoadCategories();

            // Find the book by its ID
            var book = _context.Bookss.Find(id);

            // If the book doesn't exist, return NotFound
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Find the book by its ID
            var book = _context.Bookss.Find(id);

            // If the book doesn't exist, return NotFound
            if (book == null)
            {
                return NotFound();
            }

            // Remove the book from the context and save changes
            _context.Bookss.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
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
