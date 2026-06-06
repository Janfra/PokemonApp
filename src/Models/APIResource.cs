using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class APIResource
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}