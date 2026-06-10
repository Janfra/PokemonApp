
namespace PokemonApp.Interop
{
    public interface ILocalStorageInterop
    {
        Task ClearData();
        Task<string> GetItem(string key);
        Task RemoveItem(string key);
        Task SetItem(string key, string value);
    }
}