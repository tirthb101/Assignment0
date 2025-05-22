using Microsoft.AspNetCore.Mvc;
using Assignment0.Models;
using Assignment0.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IO;

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
            Console.WriteLine("Registration method called");
            if (ModelState.IsValid)
            {   Console.WriteLine("Model is valid");
                string uniqueFileName = null;
                var fileName = model.ProfilePicture.FileName;
                var extension = Path.GetExtension(fileName).ToLower();

                if (model.ProfilePicture.Length > 0 && (extension != ".jpg" || extension != ".jpeg"))
                {
                    Console.WriteLine("File is not null");

                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfilePicture.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ProfilePicture.CopyTo(fileStream);
                    }

                }
                Console.WriteLine(uniqueFileName);

                UserAccount acount = new UserAccount
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Gender = model.Gender,
                    Education = model.Education,
                    MobileNumber = model.MobileNumber,
                    ProfilePicturePath = uniqueFileName,
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
                    ModelState.AddModelError("", "Email ID or Name Already Exists");
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
            var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            ViewBag.Email = email;

            var user = _context.UserAccounts.Where(u => u.Email == email).FirstOrDefault();
            ViewBag.ProfilePicturePath = user.ProfilePicturePath;

            return View();
        }


    }
}
