using Date_Management_Project.Data;
using Microsoft.AspNetCore.Mvc;

namespace Date_Management_Project.Controllers
{
    public class DateCalculatorController : Controller
    {
        // dependency injection of the ApplicationDbContext
        private readonly ApplicationDbContext _context;  
        public DateCalculatorController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewData["Countries"] = _context.Countries.OrderBy(country => country.Name).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateTime startDate, DateTime endDate, int countryId) // må stemma øve ens me d som står i "Form"
        {
                                                          // variabelNavn => Kolonnen du velger å sortere
            ViewData["Countries"] = _context.Countries.OrderBy(country => country.Name).ToList();
            ViewData["StartDate"] = startDate.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate.ToString("yyyy-MM-dd");

            if (startDate > endDate)
            {
                ViewData["resaraUUUUUlt"] = "Start date cannot be after end date.";
                return View("Index");
            }

            TimeSpan difference = endDate - startDate;

            // Calculate the number of working days
            int totalDays = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Check if the day is a weekend (Saturday or Sunday)
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Check if the day is a holiday in the selected country
                    if (!Holiday.IsAHoliday(date, _context, countryId))
                    {  //klasse  metode  valgtdato database  valgtland
                        totalDays++;
                    }
                }
            }

            ViewData["resaraUUUUUlt"] = $"{totalDays} working days";
            return View("Index");
        }
    }
}
