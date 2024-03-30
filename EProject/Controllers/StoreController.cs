﻿using EProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult Index(string searchString, string minPrice, string maxPrice, int? categoryId)
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
            if (categoryId.HasValue)
            {
                searchBooks = searchBooks.Where(b => b.CategoryId == categoryId.Value);
            }

            var books = searchBooks.Include(b => b.Categories).ToList();

            var categories = _context.Categories.ToList();

            ViewBag.Categories = categories;

            return View(books);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Bookss.Include(b => b.Categories).FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
