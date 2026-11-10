
using Microsoft.AspNetCore.Mvc;
using UniversityCafeteriaTask6.Data;
using UniversityCafeteriaTask6.Models;

namespace UniversityCafeteriaTask6.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();

            ViewBag.Orders = _context.Orders.ToList();

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var employee = _context.Employees.Find(id);

            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
