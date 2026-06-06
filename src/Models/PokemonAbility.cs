using System.Text.Json.Serialization;

namespace PokemonApp.Models;

public class PokemonAbility
{
    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }

    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("ability")]
    public Ability Ability { get; set; } = null!;
}
