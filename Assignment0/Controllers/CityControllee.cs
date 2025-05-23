using Assignment0.Entities;
using Assignment0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment0.Controllers
{
    public class CityController : Controller
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult City()
        {
            ViewBag.States = _context.StateTables
                .Include(s => s.Country)
                .Select(s => new { s.Id, s.StateName, CountryName = s.Country.CountryName })
                .ToList();

            ViewBag.Cities = _context.CityTables
                .Include(c => c.State)
                .ThenInclude(s => s.Country)
                .Select(c => new {
                    c.CityName,
                    StateName = c.State.StateName,
                    CountryName = c.State.Country.CountryName
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult City(CityViewModel model)
        {
            ViewBag.States = _context.StateTables
                .Include(s => s.Country)
                .Select(s => new { s.Id, s.StateName, CountryName = s.Country.CountryName })
                .ToList();

            if (ModelState.IsValid)
            {
                bool exists = _context.CityTables
                    .Any(c => c.CityName.ToLower() == model.CityName.ToLower()
                           && c.StateId == model.StateId);

                if (exists)
                {
                    ModelState.AddModelError("", "City already exists in this state.");
                }
                else
                {
                    var city = new CityTable
                    {
                        CityName = model.CityName,
                        StateId = model.StateId
                    };

                    _context.CityTables.Add(city);
                    _context.SaveChanges();

                    ViewBag.Message = "City added successfully.";
                    ModelState.Clear();
                }
            }

            ViewBag.Cities = _context.CityTables
                .Include(c => c.State)
                .ThenInclude(s => s.Country)
                .Select(c => new {
                    c.CityName,
                    StateName = c.State.StateName,
                    CountryName = c.State.Country.CountryName
                }).ToList();

            return View(new CityViewModel());
        }
    }
}
