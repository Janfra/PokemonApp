using Microsoft.AspNetCore.Http.HttpResults;
using PokemonApp.Models;
using System.Collections.Concurrent;

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

        var pokemons = new ConcurrentBag<Pokemon>();
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 5 };

        await Parallel.ForEachAsync(response.Results, parallelOptions, async (resource, cancellationToken) =>
        {
            var p = await _httpClient.GetFromJsonAsync<Pokemon>(new Uri(resource.Url), cancellationToken);
            if (p is not null)
            {
                pokemons.Add(p);
            }
        });

        if (pokemons.IsEmpty)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new PageResult<Pokemon>
        {
            Result = pokemons.OrderBy(p => p.Id).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = response.Count,
        });
    }
}
