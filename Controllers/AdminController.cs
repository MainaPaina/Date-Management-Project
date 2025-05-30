using Date_Management_Project.Data;
using Microsoft.AspNetCore.Mvc;

namespace Date_Management_Project.Controllers
{
    public class AdminController : Controller
    {

        // dependency injection of the ApplicationDbContext
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
