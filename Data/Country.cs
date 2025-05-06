using System.ComponentModel.DataAnnotations;

namespace Date_Management_Project.Data
{
    public class Country
    {
        [Key]  // Primary Key heter bare Key
        public int ID { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }
    }
}
