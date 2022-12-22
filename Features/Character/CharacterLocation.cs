using Newtonsoft.Json;

namespace Labb.Features.Character
{
    public class CharacterLocation
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}
