using Date_Management_Project.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Date_Management_Project.Models
{
    public class DateCalculatorModel
    {
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
     
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int CountryId { get; set; }

        
        public bool IncludeSaturdays { get; set; } = false;
        
        public bool IncludeSundays { get; set; } = false;
        
        public bool IncludeHolidays { get; set; } = false;


        public int CalculateDays(ApplicationDbContext _context)
        {
            // Calculate the number of days between StartDate and EndDate
            int totalDays = (EndDate - StartDate).Days + 1; // +1 to include both start and end dates
            
            // Check if the country has holidays
            Country? country = _context.Countries.Find(CountryId);
            if (country != null && !IncludeHolidays)
            {
                totalDays -= _context.Holidays.Count(h => 
                h.CountryID == CountryId && 
                h.Date >= StartDate && 
                h.Date <= EndDate);
            }

            // Check if Saturdays and Sundays should be included
            if (!IncludeSaturdays)
            {
                totalDays -= Enumerable.Range(0, (EndDate - StartDate).Days + 1)
                    .Select(d => StartDate.AddDays(d))
                    .Count(date => date.DayOfWeek == DayOfWeek.Saturday);
            }
            if (!IncludeSundays)
            {
                totalDays -= Enumerable.Range(0, (EndDate - StartDate).Days + 1)
                    .Select(d => StartDate.AddDays(d))
                    .Count(date => date.DayOfWeek == DayOfWeek.Sunday);
            }
            return totalDays;

            //TimeSpan difference = formModel.EndDate - formModel.StartDate;

            //// Calculate the number of working days
            //int totalDays = 0;
            //for (DateTime date = formModel.StartDate; date <= formModel.EndDate; date = date.AddDays(1))
            //{
            //    // Check if the day is a weekend (Saturday or Sunday)
            //    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        // Check if the day is a holiday in the selected country
            //        if (!Holiday.IsAHoliday(date, _context, formModel.CountryId))
            //        {  //klasse  metode  valgtdato database  valgtland
            //            totalDays++;
            //        }
            //    }
            //}
        }
    }
}
