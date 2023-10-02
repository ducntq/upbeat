using System.Text.Json.Serialization;

namespace upbeat.Entities {
    public class SiteConfig {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("url")]
        public required string Url { get; set; }
        [JsonPropertyName("pattern")]
        public required string Pattern { get; set; }
    }
}