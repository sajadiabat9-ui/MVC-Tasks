using Microsoft.AspNetCore.Mvc;
using UniversityCafeteriaTask6.Data;
using UniversityCafeteriaTask6.Models;

namespace UniversityCafeteriaTask6.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context; //بعرف الداتابيس كونتكست

        public CategoryController(ApplicationDbContext context) // بعمل كونستركتر 
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();

            return View(categories);
           
        }

        // Display Create form
        public IActionResult Create()
        {
            return View();
        }

        // Handle Create form submission

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
