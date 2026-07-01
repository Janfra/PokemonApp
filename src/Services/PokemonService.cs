using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using PokemonApp.Models;
using System.Collections.Concurrent;

namespace PokemonApp.Services;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient;
    private readonly MemoryCache _pokemonCache;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        var memoryOptions = new MemoryCacheOptions();
        memoryOptions.ExpirationScanFrequency = TimeSpan.FromMinutes(3);
        _pokemonCache = new MemoryCache(memoryOptions);
    }

    public async Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(string name)
    {
        string sanitisedName = name
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace("'", "");

        if (_pokemonCache.TryGetValue(sanitisedName, out Pokemon? cachedPokemon))
        {
            return TypedResults.Ok(cachedPokemon);
        }   

        var pokemon = await _httpClient.GetFromJsonAsync<Pokemon>($"pokemon/{sanitisedName}");
        if (pokemon is null)
        {
            return TypedResults.NotFound();
        }

        _pokemonCache.Set(sanitisedName, pokemon);
        return TypedResults.Ok(pokemon);
    }

    public async Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(int id)
    {
        if (_pokemonCache.TryGetValue(id, out Pokemon? cachedPokemon))
        {
            return TypedResults.Ok(cachedPokemon);
        }

        var pokemon = await _httpClient.GetFromJsonAsync<Pokemon>($"pokemon/{id}");
        if (pokemon is null)
        {
            return TypedResults.NotFound();
        }

        _pokemonCache.Set(id, pokemon);
        return TypedResults.Ok(pokemon);
    }

    public async Task<Results<Ok<PageResult<Pokemon>>, NotFound>> GetPokemonsAsync(PageRequest request)
    {
        var response = await _httpClient.GetFromJsonAsync<NamedAPIResourceList>($"pokemon/?limit={request.PageSize}&offset={request.GetOffset()}");
        if (response is null || response.Results is null) 
        {
            return TypedResults.NotFound();
        }

        var pokemons = new ConcurrentBag<Pokemon>();
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 5 };

        await Parallel.ForEachAsync(response.Results, parallelOptions, async (resource, cancellationToken) =>
        {
            if (_pokemonCache.TryGetValue(resource.Name, out Pokemon? cachedPokemon))
            {
                pokemons.Add(cachedPokemon!);
                return;
            }

            var p = await _httpClient.GetFromJsonAsync<Pokemon>(new Uri(resource.Url), cancellationToken);
            if (p is not null)
            {
                pokemons.Add(p);
            }
            _pokemonCache.Set(resource.Name, p);
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
