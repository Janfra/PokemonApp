using Microsoft.AspNetCore.Http.HttpResults;
using PokemonApp.Models;

namespace PokemonApp.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(string name)
    {
        var pokemon = await _httpClient.GetFromJsonAsync<Pokemon>($"pokemon/{name.ToLower()}");
        if (pokemon is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(pokemon);
    }

    public async Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(int id)
    {
        var pokemon = await _httpClient.GetFromJsonAsync<Pokemon>($"pokemon/{id}");
        if (pokemon is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(pokemon);
    }

    public async Task<Results<Ok<List<Pokemon>>, NotFound>> GetPokemonsAsync(int length = 10, int fromId = 0)
    {
        var response = await _httpClient.GetFromJsonAsync<NamedAPIResourceList>($"pokemon/?limit={length}&offset={fromId}");

        if (response is null) 
        {
            return TypedResults.NotFound();
        }

        var pokemonListRequests = response.Results.Select(r => _httpClient.GetFromJsonAsync<Pokemon>(new Uri(r.Url)));
        var listResult = await Task.WhenAll(pokemonListRequests);

        List<Pokemon> pokemons = listResult.Where(p => p is not null).Cast<Pokemon>().ToList();
        return TypedResults.Ok(pokemons);
    }
}
