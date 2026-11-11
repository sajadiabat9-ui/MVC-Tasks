
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using task8.Models;

namespace task8.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                var obj = new IdentityUser()
                {
                    UserName = register.Email,
                    Email = register.Email
                };

                var result = await _userManager.CreateAsync(
                    obj, register.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(
                        obj,
                        new System.Security.Claims.Claim(
                            "FullName", register.FullName));

                    var roleResult = await _userManager.AddToRoleAsync(
                        obj, "User");

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }

                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(register);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(
                    login.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        user, login.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(
                    "", "Invalid email or password");
            }

            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
