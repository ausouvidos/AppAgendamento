using System.Text.Json.Serialization;

namespace Models
{
    public class Location
    {
        [JsonPropertyName("country_name")]
        public string Country { get; set; }

        [JsonPropertyName("region_name")]
        public string Region { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
