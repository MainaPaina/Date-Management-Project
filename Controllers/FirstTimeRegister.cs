using Date_Management_Project.Data;
using Date_Management_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Date_Management_Project.Controllers
{
    public class FirstTimeRegister : Controller
    {
        private string activationKey = "815-493-00"; // Example activation password, replace with your logic

        // dependency injection of the ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FirstTimeRegister(
            ApplicationDbContext context, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (_context.Users.Any())
                return StatusCode(418, "You are not authorized to access this page.");

            return View();
        }

        [HttpPost]
        public IActionResult Index(FirstTimeRegisterModel model)
        {
            if (_context.Users.Any())
                return StatusCode(418, "You are not authorized to access this page.");
            if (string.IsNullOrEmpty(model.ActivationPassword))
                return BadRequest("Activation password is required.");

            if (string.IsNullOrEmpty(model.Email))
                return BadRequest("Email is required.");
            if (string.IsNullOrEmpty(model.Password))
                return BadRequest("Password is required.");
            if (model.Password != model.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            // Here you would typically check the activation password against a stored value

            if (model.ActivationPassword.Equals(activationKey))
            {

                User user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                };

                // Detta e ein Identity ting
                // Opprette brukeren
                var result = _userManager.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    // Optionally, assign a role to the user
                    if (!_roleManager.RoleExistsAsync("Admin").Result)
                    {
                        _roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                    }
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                    
                    // Send brukeren til innloggingsskjermen
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return BadRequest(result.Errors);
                }

            }

            return StatusCode(418);
        }
    }
}
