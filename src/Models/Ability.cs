using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class Ability
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]  
    public string Name { get; set; } = null!;

    [JsonPropertyName("pokemon")]
    public ICollection<PokemonAbility> Pokemon { get; set; } = null!;
}