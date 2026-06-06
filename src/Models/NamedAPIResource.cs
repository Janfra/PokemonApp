using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class NamedAPIResource
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
