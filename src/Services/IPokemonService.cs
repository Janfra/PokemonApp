using Microsoft.AspNetCore.Http.HttpResults;
using PokemonApp.Models;

namespace PokemonApp.Services;

public interface IPokemonService
{
    public Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(string name);
    public Task<Results<Ok<Pokemon>, NotFound>> GetPokemonAsync(int id); 
    public Task<Results<Ok<PageResult<Pokemon>>, NotFound>> GetPokemonsAsync(PageRequest request);
}
