using Microsoft.AspNetCore.Mvc;

namespace test.Controllers
{
    public class CafeteriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult products()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(string productName)
        //{
        //    return RedirectToAction("products");
        //}

        public IActionResult Google()
        {
            return Redirect("https://www.google.com");
        }
    }
}

