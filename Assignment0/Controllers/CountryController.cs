using Assignment0.Entities;
using Assignment0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment0.Controllers
{
    public class CountryController : Controller
    {
        private readonly AppDbContext _context;
      

        public CountryController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Country()
        { 
            ViewBag.Countries = _context.CountryTables.Select(c => c.CountryName).ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Country(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if country already exists (case-insensitive)
                bool exists = _context.CountryTables
                                      .Any(c => c.CountryName.ToLower() == model.CountryName.ToLower());

                if (exists)
                {
                    ModelState.AddModelError("", "Country already exists.");
                }
                else
                {
                    CountryTable countryTable = new CountryTable
                    {
                        CountryName = model.CountryName
                    };

                    _context.CountryTables.Add(countryTable);
                    _context.SaveChanges();

                    ViewBag.Message = "Country Added Successfully";
                    ModelState.Clear(); // Clear old form data
                }
            }

            // Always return updated list of countries
            ViewBag.Countries = _context.CountryTables
                                        .Select(c => c.CountryName)
                                        .ToList();

            return View(new CountryViewModel()); // Or return View(model) if you prefer
        }

    }
}
