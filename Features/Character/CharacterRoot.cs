using Newtonsoft.Json;

namespace Labb.Features.Character
{
    public class CharacterRoot
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("species")]
        public string? Species { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("origin")]
        public CharacterOrigin? Origin { get; set; }

        [JsonProperty("location")]
        public CharacterLocation? Location { get; set; }

        [JsonProperty("image")]
        public string? Image { get; set; }

        [JsonProperty("episode")]
        public List<string>? Episode { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
}
