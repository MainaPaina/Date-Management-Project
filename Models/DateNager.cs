using System.ComponentModel.DataAnnotations;

namespace Date_Management_Project.Models
{
    public class DateNager
    {
        //
        //  "date": "2017-01-01",
        //  "localName": "Neujahr",
        //  "name": "New Year's Day",
        //  "countryCode": "AT",
        //  "fixed": true,
        //  "global": true,
        //  "counties": null,
        //  "launchYear": 1967,
        //  "types": [
        //     "Public"
        //  ]
        //

        [System.Text.Json.Serialization.JsonPropertyName("date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("localName")]
        public string LocalName { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("countryCode")]
        public string CountryCode { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("fixed")]
        public bool Fixed { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("global")]
        public bool Global { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("types")]
        public List<string> Types { get; set; } = new List<string>();
    }
}
