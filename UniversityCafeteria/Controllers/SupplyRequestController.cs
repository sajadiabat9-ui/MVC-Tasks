using Microsoft.AspNetCore.Mvc;
using UniversityCafeteria.Models;
using System.Text.Json;


namespace UniversityCafeteria.Controllers
{
    public class SupplyRequestController : Controller
    {
        static List<SupplyRequest> requests = new List<SupplyRequest>();

        [HttpGet]
        public IActionResult Index()
        {
            string priority = Request.Cookies["PreferredPriority"];

            ViewBag.PreferredPriority = priority;

            return View();
        }


        [HttpPost]
        public IActionResult AddItem(string productName, string category, int quantity, decimal unitPrice, string priority)
        {
            if (string.IsNullOrEmpty(productName))
            {
                return Content("Product Name is required");
            }

            if (quantity <= 0)
            {
                return Content("Quantity must be greater than zero");
            }

            if (unitPrice <= 0)
            {
                return Content("Unit Price must be greater than zero");
            }

            // Get the old products from Session
            string data = HttpContext.Session.GetString("Draft");

            List<SupplyItem> products;

            if (data == null)
            {
                products = new List<SupplyItem>();
            }
            else
            {
                products = JsonSerializer.Deserialize<List<SupplyItem>>(data);
            }

            // Create a new product
            SupplyItem item = new SupplyItem();

            item.ProductName = productName;
            item.Category = category;
            item.Quantity = quantity;
            item.UnitPrice = unitPrice;
            item.Priority = priority;

            // Add the new product
            products.Add(item);

            // Save the list in Session
            string json = JsonSerializer.Serialize(products);

            HttpContext.Session.SetString("Draft", json);

            return RedirectToAction("Index");
        }


        public IActionResult Review()
        {
            string data = HttpContext.Session.GetString("Draft");

            List<SupplyItem> products = new List<SupplyItem>();

            if (data != null)
            {
                products = JsonSerializer.Deserialize<List<SupplyItem>>(data);
            }

            decimal subtotal = 0;

            // Calculate each product total
            foreach (var item in products)
            {
                item.TotalCost = item.Quantity * item.UnitPrice;

                subtotal += item.TotalCost;
            }

            // Calculate preparation fee
            decimal preparationFee = 0;

            if (products.Count > 0)
            {
                switch (products[0].Priority)
                {
                    case "Normal":
                        preparationFee = 2;
                        break;

                    case "Urgent":
                        preparationFee = 5;
                        break;

                    case "Emergency":
                        preparationFee = 10;
                        break;
                }
            }

            // Calculate discount
            decimal discount = 0;

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }

            // Calculate final total
            decimal finalTotal = subtotal + preparationFee - discount;

            ViewBag.Subtotal = subtotal;
            ViewBag.PreparationFee = preparationFee;
            ViewBag.Discount = discount;
            ViewBag.FinalTotal = finalTotal;

            return View(products);
        }

        public IActionResult ConfirmRequest()
        {
            // Get products from Session
            string data = HttpContext.Session.GetString("Draft");

            if (data == null)
            {
                return Content("There are no products to confirm.");
            }

            List<SupplyItem> products =
                JsonSerializer.Deserialize<List<SupplyItem>>(data);

            // Calculate request information
            decimal subtotal = 0;

            foreach (var item in products)
            {
                item.TotalCost = item.Quantity * item.UnitPrice;
                subtotal += item.TotalCost;
            }

            decimal preparationFee = 0;

            switch (products[0].Priority)
            {
                case "Normal":
                    preparationFee = 2;
                    break;

                case "Urgent":
                    preparationFee = 5;
                    break;

                case "Emergency":
                    preparationFee = 10;
                    break;
            }

            decimal discount = 0;

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }

            decimal finalTotal = subtotal + preparationFee - discount;

            // Create confirmed request
            SupplyRequest request = new SupplyRequest();

            request.RequestID = requests.Count + 1;
            request.Products = products;
            request.Subtotal = subtotal;
            request.PreparationFee = preparationFee;
            request.Discount = discount;
            request.FinalTotal = finalTotal;

            // Save request in the list
            requests.Add(request);

            // Clear Session after confirmation
            HttpContext.Session.Remove("Draft");

            return RedirectToAction("MyRequests");
        }
        public IActionResult ClearDraft()
        {
            HttpContext.Session.Remove("Draft");

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult SavePreference(string priority)
        {
            CookieOptions options = new CookieOptions();

            options.Expires = DateTime.Now.AddDays(7);

            Response.Cookies.Append("PreferredPriority", priority, options);

            return RedirectToAction("Index");
        }

        public IActionResult MyRequests()
        {
            return View(requests);
        }



        public IActionResult About()
        {
            return View();
        }


    }
}
