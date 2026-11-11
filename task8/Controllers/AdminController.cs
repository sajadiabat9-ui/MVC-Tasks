
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace task8.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Display all users
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            ViewBag.Roles = _roleManager.Roles.ToList();
            ViewBag.CurrentRoles = await _userManager.GetRolesAsync(user);

            return View(user);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(roleName) ||
                !await _roleManager.RoleExistsAsync(roleName))
            {
                ModelState.AddModelError("", "Invalid role.");
            }
            else
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Prevent removing the last SuperAdmin
                if (currentRoles.Contains("SuperAdmin") &&
                    roleName != "SuperAdmin")
                {
                    var superAdmins = await _userManager.GetUsersInRoleAsync("SuperAdmin");

                    if (superAdmins.Count <= 1)
                    {
                        ModelState.AddModelError("",
                            "Cannot remove the last SuperAdmin.");
                    }
                }

                if (ModelState.IsValid)
                {
                    var removeResult =
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    if (!removeResult.Succeeded)
                    {
                        foreach (var error in removeResult.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                    else
                    {
                        var addResult =
                            await _userManager.AddToRoleAsync(user, roleName);

                        if (addResult.Succeeded)
                            return RedirectToAction("Index");

                        foreach (var error in addResult.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }
            }

            ViewBag.Roles = _roleManager.Roles.ToList();
            ViewBag.CurrentRoles = await _userManager.GetRolesAsync(user);

            return View(user);
        }

        // Delete GET
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // Delete POST
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (user.Id == _userManager.GetUserId(User))
            {
                return BadRequest("You cannot delete your own account.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("SuperAdmin"))
            {
                var superAdmins = await _userManager.GetUsersInRoleAsync("SuperAdmin");

                if (superAdmins.Count <= 1)
                    return BadRequest("Cannot delete the last SuperAdmin.");
            }

            // Requests belong to the user; prevent accidental history deletion.
            // We will handle accounts with requests before deleting them.
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View("Delete", user);
            }

            return RedirectToAction("Index");
        }
    }
}
