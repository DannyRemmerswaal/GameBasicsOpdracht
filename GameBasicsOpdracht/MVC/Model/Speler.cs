using Newtonsoft.Json;

namespace GameBasicsOpdracht.MVC.Model
{
    public class Speler
    {
        [JsonProperty("Naam")]
        public string Naam { get; set; }
        [JsonProperty("RugNummer")]
        public int RugNummer { get; set; }
        [JsonProperty("Positie")]
        public string Positie { get; set; }
        [JsonProperty("Rating")]
        public int Rating { get; set; }
        [JsonProperty("Team")]
        public string Team { get; set; }           
    }
}
