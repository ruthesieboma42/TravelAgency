namespace TravelAgency.Core.Models
{
    public class AmadeusSettings
    {
        public const string Amadeus = "Amadeus";

        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
    }
}
