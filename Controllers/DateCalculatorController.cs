using Date_Management_Project.Data;
using Date_Management_Project.Models;
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
        public IActionResult Index(DateCalculatorModel formModel) // må stemma øve ens me d som står i "Form"
        {
            if (formModel != null)
            {
                // variabelNavn => Kolonnen du velger å sortere
                ViewData["Countries"] = _context.Countries.OrderBy(country => country.Name).ToList();
                /*ViewData["StartDate"] = formModel.StartDate.ToString("yyyy-MM-dd");
                ViewData["EndDate"] = endDate.ToString("yyyy-MM-dd");*/

                if (formModel.StartDate > formModel.EndDate)
                {
                    ViewData["resaraUUUUUlt"] = "Start date cannot be after end date.";
                    return View("Index");
                }

                int totalDays = formModel.CalculateDays(_context);


                ViewData["resaraUUUUUlt"] = $"{totalDays} working days";
                return View("Index");
            }
            else
            {
                ViewData["resaraUUUUUlt"] = "Please fill in all fields.";
                return View("Index");
            }
        }



    }
}
