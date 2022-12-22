using Newtonsoft.Json;

namespace Labb.Features.Character
{
    public class CharacterOrigin
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}
