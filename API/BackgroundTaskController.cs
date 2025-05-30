using Date_Management_Project.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Date_Management_Project.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackgroundTaskController : ControllerBase
    {

        // Detta e - DEPENDENCY INJECTION

        // variabel for å holde database connection og tabeller
        private readonly ApplicationDbContext _context;

        // ved opprettelse av controlleren, tar vi inn ApplicationDbContext som parameter - dependency injection
        public BackgroundTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BackgroundTask/Run
        [HttpGet("Run")]
        
        public async Task<IActionResult> RunBackgroundTaskAsync()
        {
            // This method is intended to be called to run a background task
            // Can be called by calling for /api/BackgroundTask/Run


            // pull data from api
            // For hvert land i database, hent helligdager for forrige, gjeldende og neste år fra API.
            foreach (Country country in await _context.Countries.ToListAsync())
            {
                List<Models.DateNager> publicHolidays = new List<Models.DateNager>();
                // Get holidays for the previous year, current year, and next year for each country
                List<Models.DateNager>? list1 = await Holiday.GetHolidaysFromAPIAsync(DateTime.Now.Year - 1, country.CountryCode);
                List<Models.DateNager>? list2 = await Holiday.GetHolidaysFromAPIAsync(DateTime.Now.Year + 0, country.CountryCode);
                List<Models.DateNager>? list3 = await Holiday.GetHolidaysFromAPIAsync(DateTime.Now.Year + 1, country.CountryCode);
                // Combine the lists into one
                if (list1 != null) publicHolidays.AddRange(list1);
                if (list2 != null) publicHolidays.AddRange(list2);
                if (list3 != null) publicHolidays.AddRange(list3);

                if (publicHolidays != null && publicHolidays.Count > 0)
                {
                    // Sjekk om helligdag eksisterer i database, hvis ikke - legg den til!
                    foreach (Models.DateNager holiday in publicHolidays)
                    {
                        // Check if the holiday already exists in the database
                        bool exists = await _context.Holidays.AnyAsync(h => h.Date.Date == holiday.Date && h.CountryID == country.ID);
                        if (!exists)
                        {
                            // If it doesn't exist, add it to the database
                            Holiday newHoliday = new Holiday
                            {
                                Name = holiday.LocalName,
                                Date = holiday.Date,
                                CountryID = country.ID
                            };
                            _context.Holidays.Add(newHoliday);
                            HttpContext.Response.WriteAsync($"Added holiday: {holiday.LocalName} on {holiday.Date.ToShortDateString()} for country: {country.Name}\n");
                        }
                    }
                    int changes = _context.SaveChanges();
                    HttpContext.Response.WriteAsync($"Saved {changes} changes to the database for country: {country.Name}\n");
                }
                else
                {
                    HttpContext.Response.WriteAsync($"No public holidays found for country: {country.Name}({country.CountryCode} for the years {DateTime.Now.Year - 1}, {DateTime.Now.Year}, and {DateTime.Now.Year + 1}.\n");
                }
                    publicHolidays.Clear();
            }

           




            return Ok("Background task executed successfully.");
        }

    }
}
