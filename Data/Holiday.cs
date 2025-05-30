using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

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




        public async static Task<List<Models.DateNager>?> GetHolidaysFromAPIAsync(int Year, string CountryCode)
        {
            // This method is intended to be called to get holidays from an API

            HttpClient WC = new HttpClient(); //.Create("https://date.nager.at/api/v3/publicholidays/2025/AT");
            Uri url = new Uri($"https://date.nager.at/api/v3/publicholidays/{Year}/{CountryCode}");
            
            HttpResponseMessage response = await WC.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // If the response is successful, read the content as a string
                string json = await response.Content.ReadAsStringAsync();
                // Deserialize the JSON string into a list of Holiday objects
                var nagerList = System.Text.Json.JsonSerializer.Deserialize<List<Models.DateNager>>(json);
                return nagerList;

            }
            else
            {
                // Handle the error case if needed
                throw new Exception($"Error fetching holidays: {response.StatusCode}");
            }
        }






        // method for checking if a date is a holiday
        public static bool IsAHoliday(DateTime date, ApplicationDbContext db, int CountryId)
        {
            // Check if the date is a holiday in the specified country (Any - sjekker om det er noen som matcher)
            return db.Holidays.Any(holiday => holiday.Date.Date == date.Date && holiday.CountryID == CountryId);
        }
    }
}