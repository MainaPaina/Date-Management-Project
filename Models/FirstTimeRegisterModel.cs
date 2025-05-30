using System.ComponentModel.DataAnnotations;

namespace Date_Management_Project.Models
{
    public class FirstTimeRegisterModel
    {

        [Required]
        public string ActivationPassword { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
