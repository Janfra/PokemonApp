using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class NamedAPIResourceList
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; } = string.Empty;

    [JsonPropertyName("previous")]
    public string Previous {  get; set; } = string.Empty;

    [JsonPropertyName("results")]
    public List<NamedAPIResource> Results { get; set; } = null!;
}