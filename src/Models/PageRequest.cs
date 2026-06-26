namespace PokemonApp.Models;

public class PageRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int GetOffset() => (Page - 1) * PageSize;
}