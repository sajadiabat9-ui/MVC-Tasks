
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityCafeteriaTask6.Data;
using UniversityCafeteriaTask6.Models;

namespace UniversityCafeteriaTask6.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");

            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
