using AcademyGraduationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademyGraduationSystem.Controllers
{
    public class SupplyController : Controller
    {
        private List<Category> categories = new List<Category>()
{
    new Category { Id = 1, Name = "Graduation Certificates" },

    new Category { Id = 2, Name = "Hospitality" },

    new Category { Id = 3, Name = "Graduation Gifts" }
};
        private List<Supplier> suppliers = new List<Supplier>()
{
    new Supplier { Id = 1, Name = "Supplier A", CategoryId = 1 },
    new Supplier { Id = 2, Name = "Supplier B", CategoryId = 1 },
    new Supplier { Id = 3, Name = "Supplier C", CategoryId = 1 },

    new Supplier { Id = 4, Name = "Supplier D", CategoryId = 2 },
    new Supplier { Id = 5, Name = "Supplier E", CategoryId = 2 },
    new Supplier { Id = 6, Name = "Supplier F", CategoryId = 2 },

    new Supplier { Id = 7, Name = "Supplier G", CategoryId = 3 },
    new Supplier { Id = 8, Name = "Supplier H", CategoryId = 3 },
    new Supplier { Id = 9, Name = "Supplier I", CategoryId = 3 }
};
        private List<Offer> offers = new List<Offer>()
{
    new Offer { Id = 1, Name = "Premium Certificate Package", Quantity = 35, UnitPrice = 2, SupplierId = 1 },

    new Offer { Id = 2, Name = "Standard Certificate Package", Quantity = 35, UnitPrice = 1.5m, SupplierId = 2 },

    new Offer { Id = 3, Name = "Luxury Certificate Package", Quantity = 35, UnitPrice = 2.5m, SupplierId = 3 },

    new Offer { Id = 4, Name = "Coffee & Drinks Package", Quantity = 100, UnitPrice = 2, SupplierId = 4 },

    new Offer { Id = 5, Name = "Sandwiches & Snacks Package", Quantity = 100, UnitPrice = 3, SupplierId = 5 },

    new Offer { Id = 6, Name = "Full Hospitality Package", Quantity = 100, UnitPrice = 4, SupplierId = 6 },

    new Offer { Id = 7, Name = "Customized Mugs Package", Quantity = 35, UnitPrice = 2, SupplierId = 7 },

    new Offer { Id = 8, Name = "Graduation Bags Package", Quantity = 35, UnitPrice = 3, SupplierId = 8 },

    new Offer { Id = 9, Name = "Premium Gift Boxes Package", Quantity = 35, UnitPrice = 4, SupplierId = 9 }
};

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Categories = categories;

            ViewBag.Suppliers = suppliers;

            ViewBag.Offers = offers;

            return View();
            
        }
        [HttpPost] //هاد الي حيستقبل رقم الاوفر لما نضغط سليكت
        public IActionResult SelectOffer(int offerId)
        {

            int categoryId = 0;

            foreach (var offer in offers) 
            {
                if (offer.Id == offerId)
                {
                    foreach (var supplier in suppliers)
                    {
                        if (supplier.Id == offer.SupplierId)  // بعد ما لقيناه ببعرض بحثنا عن المورد التابع إل
                        {
                            categoryId = supplier.CategoryId; //أخذنا CategoryId من المورد
                        }
                    }
                }
            }

            if (categoryId == 1)
            {
                HttpContext.Session.SetInt32("Category1Offer", offerId); // خزننا رقم الاوفر الي اختارو الموظف في السيشن
            }
            else if (categoryId == 2)
            {
                HttpContext.Session.SetInt32("Category2Offer", offerId);
            }
            else if (categoryId == 3)
            {
                HttpContext.Session.SetInt32("Category3Offer", offerId);
            }


            TempData["Message"] = "Offer selected successfully!"; // رسالة نجاح بعد ما اختارو الاوفر ودمررها للفيو 
            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Review()
        {
            int? category1 = HttpContext.Session.GetInt32("Category1Offer");
            int? category2 = HttpContext.Session.GetInt32("Category2Offer");
            int? category3 = HttpContext.Session.GetInt32("Category3Offer");

            if (category1 == null || category2 == null || category3 == null)
            {
                TempData["Message"] = "Please select one offer from each category first.";
                return RedirectToAction("Index");
            }
            ViewBag.Category1 = category1;
            ViewBag.Category2 = category2;
            ViewBag.Category3 = category3;

            ViewBag.Offers = offers;
            ViewBag.Suppliers = suppliers;



            decimal totalPrice = 0;

            foreach (var offer in offers)
            {
                if (offer.Id == category1 ||
                    offer.Id == category2 ||
                    offer.Id == category3)
                {
                    totalPrice += offer.Quantity * offer.UnitPrice;
                }
            }

            ViewBag.TotalPrice = totalPrice;




            decimal discountPercentage = 0;

            if (totalPrice > 500)
            {
                discountPercentage = 0.15m;
            }
            else if (totalPrice > 300)
            {
                discountPercentage = 0.10m;
            }
            else
            {
                discountPercentage = 0;
            }

            ViewBag.DiscountPercentage = discountPercentage;




            decimal discountAmount = totalPrice * discountPercentage;

            ViewBag.DiscountAmount = discountAmount;




            decimal finalTotal = totalPrice - discountAmount;

            ViewBag.FinalTotal = finalTotal;
            return View();
        }



        public IActionResult About()
        {
            return View();
        }
    }
}
