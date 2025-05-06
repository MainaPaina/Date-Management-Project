using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Date_Management_Project.Data
{
    public class Holiday
    {
        [Key]  // Primary Key heter bare Key
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;  // viss ishe blir "Name" null og programme kan kræsja viss du ishe e ferdi

        public DateTime Date { get; set; }

        [ForeignKey("Country")]
        public int CountryID { get; set; }

        public Country? Country { get; set; }











        // method for checking if a date is a holiday
        public static bool IsAHoliday(DateTime date, ApplicationDbContext db, int CountryId)
        {
            // Check if the date is a holiday in the specified country (Any - sjekker om det er noen som matcher)
            return db.Holidays.Any(holiday => holiday.Date.Date == date.Date && holiday.CountryID == CountryId);
        }
    }
}