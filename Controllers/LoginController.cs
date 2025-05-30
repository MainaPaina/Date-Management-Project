using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Date_Management_Project.Data;
using System.Threading.Tasks;

namespace Date_Management_Project.Controllers
{
    public class LoginController : Controller
    {

        private readonly SignInManager<User> _signinmanager;
        private readonly UserManager<User> _usermanager;
        private readonly ApplicationDbContext _context;

        public LoginController(SignInManager<User> signinmanager, UserManager<User> usermanager, ApplicationDbContext context)
        {
            _signinmanager = signinmanager;
            _usermanager = usermanager;
            _context = context;
        }

        public IActionResult Index()
        {
            if (_context.Users.Any() == false)
                return RedirectToAction("Index", "FirstTimeRegister");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Models.LoginModel model)
        {

            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("Email is required");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Password is required");
            }

            // hent bruker
            var user = _usermanager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                return BadRequest("User or password is wrong");
            }

            await _signinmanager.SignInAsync(user, model.RememberMe);

            if (_signinmanager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            

            return View(model);
        }
    }
}
