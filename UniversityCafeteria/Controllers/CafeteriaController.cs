using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class CafeteriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult product()
        {
            return View();
        }

       
        public IActionResult Suppliers()
        {
            return View();
        }

        public IActionResult SupplyStatus() 
        {
            return View();
        }

    }
}
