using Assignment0.Entities;
using Assignment0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment0.Controllers
{
    public class StateController : Controller
    {
        private readonly AppDbContext _context;

        public StateController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult State()
        {
            ViewBag.Countries = _context.CountryTables
                                        .Select(c => new { c.Id, c.CountryName })
                                        .ToList();

            ViewBag.States = _context.StateTables
                              .Include(s => s.Country)
                                     .Select(s => new
                                     {
                                         s.StateName,
                                         CountryName = s.Country.CountryName
                                     }).ToList();

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult State(StateViewModel model)
        {
            ViewBag.Countries = _context.CountryTables
                                        .Select(c => new { c.Id, c.CountryName })
                                        .ToList();

            if (ModelState.IsValid)
            {
                bool exists = _context.StateTables
                                      .Any(s => s.StateName.ToLower() == model.StateName.ToLower()
                                             && s.CountryId == model.CountryId);

                if (exists)
                {
                    ModelState.AddModelError("", "State already exists in this country.");
                }
                else
                {
                    var state = new StateTable
                    {
                        StateName = model.StateName,
                        CountryId = model.CountryId
                    };

                    _context.StateTables.Add(state);
                    _context.SaveChanges();

                    ViewBag.Message = "State added successfully.";
                    ModelState.Clear();
                }
            }

            ViewBag.States = _context.StateTables
                                     .Include(s => s.Country)
                                     .Select(s => new
                                     {
                                         s.StateName,
                                         CountryName = s.Country.CountryName
                                     }).ToList();

            return View(new StateViewModel());
        }
    }
}
