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

    public async Task<Results<Ok<PageResult<Pokemon>>, NotFound>> GetPokemonsAsync(PageRequest request)
    {
        var response = await _httpClient.GetFromJsonAsync<NamedAPIResourceList>($"pokemon/?limit={request.PageSize}&offset={request.GetOffset()}");
        if (response is null || response.Results is null) 
        {
            return TypedResults.NotFound();
        }

        var pokemonListRequests = response.Results.Select(r => _httpClient.GetFromJsonAsync<Pokemon>(new Uri(r.Url)));
        var listResult = await Task.WhenAll(pokemonListRequests);
        List<Pokemon> pokemons = listResult.Where(p => p is not null).Cast<Pokemon>().ToList();
        if (pokemons.Count <= 0)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new PageResult<Pokemon>
        {
            Result = pokemons,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = response.Count,
        });
    }
}
