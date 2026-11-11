
using Microsoft.AspNetCore.Mvc;
using UniversityCafeteriaTask6.Data;
using UniversityCafeteriaTask6.Models;

namespace UniversityCafeteriaTask6.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();

            ViewBag.Products = _context.Products.ToList();

            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            return View(supplier);
        }

        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            return View(supplier);
        }

        public IActionResult Delete(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            var products = _context.Products.Where(p => p.SupplierId == id).ToList();
            foreach (var product in products)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
