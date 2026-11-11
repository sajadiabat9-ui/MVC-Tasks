
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task8.Data;
using task8.Models;

namespace task8.Controllers
{
    [Authorize]
    public class SupplyRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SupplyRequestController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Display requests
        public IActionResult Index()
        {
            var requests = _context.SupplyRequests
                .Include(r => r.User)
                .AsQueryable();

            if (!User.IsInRole("Admin") &&
                !User.IsInRole("SuperAdmin"))
            {
                string? userId = _userManager.GetUserId(User);

                requests = requests.Where(r => r.UserId == userId);
            }

            return View(requests.ToList());
        }

        // Create GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int[] productIds, int[] quantities)
        {
            if (productIds == null || quantities == null ||
                productIds.Length == 0 ||
                productIds.Length != quantities.Length)
            {
                ModelState.AddModelError("", "Select products and quantities.");
                ViewBag.Products = _context.Products.ToList();
                return View();
            }

            var request = new SupplyRequest
            {
                RequestDate = DateTime.Now,
                Status = "Pending",
                UserId = _userManager.GetUserId(User)!
            };

            decimal total = 0;

            for (int i = 0; i < productIds.Length; i++)
            {
                if (quantities[i] <= 0)
                    continue;

                var product = _context.Products.Find(productIds[i]);

                if (product == null || product.Quantity < quantities[i])
                {
                    ModelState.AddModelError("", "Invalid product or quantity.");
                    ViewBag.Products = _context.Products.ToList();
                    return View();
                }

                var item = new RequestItem
                {
                    ProductId = product.Id,
                    Quantity = quantities[i],
                    Price = product.Price
                };

                request.RequestItems.Add(item);
                total += product.Price * quantities[i];
            }

            if (request.RequestItems.Count == 0)
            {
                ModelState.AddModelError("", "Select at least one product.");
                ViewBag.Products = _context.Products.ToList();
                return View();
            }

            request.TotalPrice = total;

            _context.SupplyRequests.Add(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Details
        public IActionResult Details(int id)
        {
            var request = _context.SupplyRequests
                .Include(r => r.RequestItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (!User.IsInRole("Admin") &&
                !User.IsInRole("SuperAdmin") &&
                request.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(request);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var request = _context.SupplyRequests.Find(id);

            if (request == null)
                return NotFound();

            if (request.Status != "Pending")
                return BadRequest();

            if (User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
            {
                return View("Review", request);
            }

            if (request.UserId != _userManager.GetUserId(User))
                return Forbid();

            request.RequestItems = _context.RequestItems
                .Where(x => x.SupplyRequestId == id)
                .ToList();

            ViewBag.Products = _context.Products.ToList();

            return View(request);
        }


        // Edit POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, int[] productIds,
                                  int[] quantities, string? status)
        {
            var request = _context.SupplyRequests.Find(id);

            if (request == null)
                return NotFound();

            if (request.Status != "Pending")
                return BadRequest();

            // Admin / SuperAdmin
            if (User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
            {
                if (status != "Approved" && status != "Rejected")
                    return BadRequest();

                request.Status = status;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // User
            if (request.UserId != _userManager.GetUserId(User))
                return Forbid();

            if (productIds == null || quantities == null ||
                productIds.Length != quantities.Length)
                return BadRequest();

            var oldItems = _context.RequestItems
                .Where(x => x.SupplyRequestId == id)
                .ToList();

            decimal total = 0;
            var newItems = new List<RequestItem>();

            for (int i = 0; i < productIds.Length; i++)
            {
                if (quantities[i] < 0)
                    return BadRequest();

                if (quantities[i] == 0)
                    continue;

                var product = _context.Products.Find(productIds[i]);

                if (product == null)
                    return BadRequest();

                if (quantities[i] > product.Quantity)
                    return BadRequest();

                newItems.Add(new RequestItem
                {
                    SupplyRequestId = id,
                    ProductId = product.Id,
                    Quantity = quantities[i],
                    Price = product.Price
                });

                total += product.Price * quantities[i];
            }

            if (newItems.Count == 0)
                return BadRequest();

            _context.RequestItems.RemoveRange(oldItems);
            _context.RequestItems.AddRange(newItems);

            request.TotalPrice = total;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // Delete GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var request = _context.SupplyRequests.Find(id);

            if (request == null)
                return NotFound();

            if (!User.IsInRole("Admin") &&
                !User.IsInRole("SuperAdmin") &&
                request.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            if (request.Status != "Pending")
                return BadRequest("Only pending requests can be deleted.");

            return View(request);
        }

        // Delete POST
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var request = _context.SupplyRequests.Find(id);

            if (request == null)
                return NotFound();

            if (!User.IsInRole("Admin") &&
                !User.IsInRole("SuperAdmin") &&
                request.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            if (request.Status != "Pending")
                return BadRequest("Only pending requests can be deleted.");

            _context.SupplyRequests.Remove(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
