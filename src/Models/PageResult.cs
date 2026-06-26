namespace PokemonApp.Models;

public class PageResult<T>
{
    public IEnumerable<T> Result { get; set; } = null!;
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
