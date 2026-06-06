using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class Type
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]  
    public string Name { get; set; } = null!;
}