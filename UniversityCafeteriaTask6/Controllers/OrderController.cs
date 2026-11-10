
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityCafeteriaTask6.Data;
using UniversityCafeteriaTask6.Models;

namespace UniversityCafeteriaTask6.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.Employees = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Find(id);

            ViewBag.Employees = new SelectList(_context.Employees, "Id", "FullName");

            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders.Find(id);
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
