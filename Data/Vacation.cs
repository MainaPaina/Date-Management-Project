using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Date_Management_Project.Data
{
    public class Vacation
    {
        [Key]  // Primary Key heter bare Key
        public int ID { get; set; }

        public string? UserId { get; set; } // Foreign key til User table

        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; } // Foreign key til User table

        public string Name { get; set; } = string.Empty;  // viss ishe blir "Name" null og programme kan kræsja viss du ishe e ferdi

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}