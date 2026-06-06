using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class Pokemon
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("base_experience")]
    public int BaseExperience { get; set; }

    [JsonPropertyName("height")]    
    public int Height { get; set; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("abilities")]
    public ICollection<PokemonAbility> Abilities { get; set; } = null!;

    [JsonPropertyName("sprites")]
    public PokemonSprites Sprites { get; set; } = new();

    [JsonPropertyName("types")] 
    public List<PokemonType> Types { get; set; } = new();
}
