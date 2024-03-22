using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EProject.Controllers
{
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
        public IActionResult Edit(Books model)
        {
            ModelState.Remove("Categories");
            if (ModelState.IsValid)
            {
                _context.Bookss.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id != null)
            {
                NotFound();
            }
            LoadCategories();
            var product = _context.Bookss.Find(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Books model)
        {
            _context.Bookss.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
