
using Microsoft.AspNetCore.Mvc;
using UniversityCafeteriaTask6.Data;
using UniversityCafeteriaTask6.Models;

namespace UniversityCafeteriaTask6.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();

            ViewBag.Products = _context.Products.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var category = _context.Categories.Find(id);

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            var products = _context.Products.Where(p => p.CategoryId == id).ToList();
            foreach (var product in products)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
