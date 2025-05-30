using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Date_Management_Project.Data;
using System.Threading.Tasks;

namespace Date_Management_Project.Controllers
{
    public class LogoutController : Controller
    {

        private readonly SignInManager<User> _signinmanager;
        private readonly UserManager<User> _usermanager;
        private readonly ApplicationDbContext _context;

        public LogoutController(SignInManager<User> signinmanager, UserManager<User> usermanager, ApplicationDbContext context)
        {
            _signinmanager = signinmanager;
            _usermanager = usermanager;
            _context = context;
        }

        public IActionResult Index()
        {
            _signinmanager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
