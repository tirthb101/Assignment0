using Microsoft.AspNetCore.Mvc;
using Assignment0.Models;
using Assignment0.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Assignment0.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AccountController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount acount = new UserAccount
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Gender = model.Gender,
                    Education = model.Education,
                    MobileNumber = model.MobileNumber,
                };

                try
                {
                    _context.UserAccounts.Add(acount);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{model.Email} Registraterd Successful!";
                    return View();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Email ID Already Exists");
                    return View(model);
                }


            }

            return View(model);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserAccounts.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("MobileNumber", user.MobileNumber)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {

                    ModelState.AddModelError("", "Invalid Email or Password");
                    return View(model);
                }

            }

            return View(model);
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            ViewBag.Email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            return View();
        }


    }
}
