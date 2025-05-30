using Microsoft.AspNetCore.Identity;

namespace Date_Management_Project.Data
{
    public class User : IdentityUser
    {
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
