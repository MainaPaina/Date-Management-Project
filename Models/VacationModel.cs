using Microsoft.AspNetCore.Identity;
using Date_Management_Project.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Date_Management_Project.Models
{
    public class VacationModel
    {
        public int ID { get; set; }

        public string? UserId { get; set; } // Foreign key til User table
        public User? User { get; set; } // Foreign key til User table
        public string Name { get; set; } = string.Empty;  // viss ishe blir "Name" null og programme kan kræsja viss du ishe e ferdi

        
        public int DaysToStart { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public int DaysToEnd { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int DaysDuration { get; set; } = 0;
    }
}
