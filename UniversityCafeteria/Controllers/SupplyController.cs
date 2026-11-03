using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {
            string[] categories =
            {
        "Drinks",
        "Food",
        "Snacks",
        "Hot Drinks",
        "Cold Drinks"
    };

            ViewBag.Categories = categories;

            return View();
        }

        public IActionResult Products()
        {
            ViewData["PageTitle"] = "Cafeteria Products";
            ViewData["ManagerName"] = "Ahmad Ali";
            ViewData["NumberOfProducts"] = 5;

            string[] products = {
        "Water",
        "Orange Juice",
        "Coffee",
        "Sandwich",
        "Chocolate"
    };

            ViewData["Products"] = products;

            return View();
        }


        public IActionResult Suppliers()
        {
            ViewBag.PageTitle = "Our Suppliers";
            ViewBag.NumberOfSuppliers = 3;

            string[] suppliers =
            {
        "Fresh Food Company",
        "Happy Drinks Company",
        "Daily Supplies Company"
    };

            ViewBag.Suppliers = suppliers;

            return View();
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult SubmitRequest()
        {
            string employeeName = Request.Form["employeeName"];
            string productName = Request.Form["productName"];
            string quantity = Request.Form["quantity"];
            string requiredDate = Request.Form["requiredDate"];

            if (string.IsNullOrEmpty(employeeName))
            {
                ViewBag.Message = "Error: Employee Name is required!";
                return View();
            }
            else
            {
                TempData["Message"] = "Request submitted successfully!";

                return RedirectToAction("About");
            }
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult CheckQuantity(int quantity)
        {
            if (quantity < 20)
            {
                return Content("Small Order");
            }
            else if (quantity <= 50)
            {
                return Content("Medium Order");
            }
            else
            {
                return Content("Large Order");
            }
        }
    }
}

