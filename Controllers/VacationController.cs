using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Date_Management_Project.Data;
using Date_Management_Project.Models;

namespace Date_Management_Project.Controllers
{
    public class VacationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vacation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vacations.Include(v => v.User);

            List<Models.VacationModel> vacations = new List<Models.VacationModel>();

            foreach (var vacation in await applicationDbContext.ToListAsync())
            {
                int daysToStart = 0;
                if (vacation.StartDate > DateTime.Now)
                {
                    DateCalculatorModel dateCalculatorModel_Start = new DateCalculatorModel
                    {
                        CountryId = vacation.User.CountryId ?? _context.Countries.First().ID,
                        StartDate = DateTime.Now,
                        EndDate = vacation.StartDate
                    };

                    daysToStart = dateCalculatorModel_Start.CalculateDays(_context);

                }

                int daysToEnd = 0;
                if (vacation.EndDate > DateTime.Now)
                {
                    DateCalculatorModel dateCalculatorModel_End = new DateCalculatorModel
                    {
                        CountryId = vacation.User.CountryId ?? _context.Countries.First(country => country.CountryCode == "NO").ID,
                        StartDate = DateTime.Now,
                        EndDate = vacation.EndDate
                    };

                    daysToEnd = dateCalculatorModel_End.CalculateDays(_context);

                }

                int daysDuration = 0;

                DateCalculatorModel dateCalculatorModel_Duration = new DateCalculatorModel
                {
                    CountryId = vacation.User.CountryId ?? _context.Countries.First().ID,
                    StartDate = vacation.StartDate,
                    EndDate = vacation.EndDate
                };

                daysDuration = dateCalculatorModel_Duration.CalculateDays(_context);

                Models.VacationModel vacationModel = new Models.VacationModel
                {
                    ID = vacation.ID,
                    UserId = vacation.UserId,
                    User = vacation.User,
                    Name = vacation.Name,
                    StartDate = vacation.StartDate,
                    EndDate = vacation.EndDate,
                    DaysToStart = daysToStart,
                    DaysToEnd = daysToEnd,
                    DaysDuration = daysDuration
                };
                vacations.Add(vacationModel);
            }


            return View(vacations);
        }

        // GET: Vacation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }

        // GET: Vacation/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Vacation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserId,Name,StartDate,EndDate")] Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vacation.UserId);
            return View(vacation);
        }

        // GET: Vacation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations.FindAsync(id);
            if (vacation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", vacation.UserId);
            return View(vacation);
        }

        // POST: Vacation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserId,Name,StartDate,EndDate")] Vacation vacation)
        {
            if (id != vacation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vacation.UserId);
            return View(vacation);
        }

        // GET: Vacation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }

        // POST: Vacation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacation = await _context.Vacations.FindAsync(id);
            if (vacation != null)
            {
                _context.Vacations.Remove(vacation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationExists(int id)
        {
            return _context.Vacations.Any(e => e.ID == id);
        }
    }
}
