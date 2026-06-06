using Microsoft.AspNetCore.Http.HttpResults;
using PokemonApp.Models;

namespace PokemonApp.Services;

public interface IPokemonService
{
    public Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(string name);
    public Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(int id); 
    public Task<Results<Ok<List<Pokemon>>, NotFound>> GetPokemonsAsync(int length = 10, int fromId = 0);
}
