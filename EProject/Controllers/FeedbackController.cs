using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EProject.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var feedbacks = _context.suggests.ToList();
            return View(feedbacks);
        }
     

        [HttpGet]
        public IActionResult Reply(int id)
        {
            var suggestion = _context.suggests.Find(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        [HttpPost]
        public IActionResult Reply(int id, string adminResponse)
        {
            var suggestion = _context.suggests.Find(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            suggestion.AdminResponse = adminResponse;
            suggestion.AdminResponseDate = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserFeedbackInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var feedback = new suggest
                {
                    CustomerName = inputModel.CustomerName,
                    Email = inputModel.Email,
                    Message = inputModel.Message,
                    CreatedAt = DateTime.Now,
                    // Set default value for AdminResponse
                    AdminResponse = string.Empty,
                    // AdminResponseDate will be null by default
                };

                _context.suggests.Add(feedback);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inputModel);
        }

    }
}
