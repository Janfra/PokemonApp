using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class PokemonType
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public Type Type { get; set; } = null!;
}
