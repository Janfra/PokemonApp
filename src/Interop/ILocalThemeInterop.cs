
namespace PokemonApp.Interop
{
    public interface ILocalThemeInterop
    {
        bool IsDarkMode { get; }

        Task SetIsDarkMode(bool isDarkMode);
        Task<bool> IsBrowserDarkMode();
    }
}