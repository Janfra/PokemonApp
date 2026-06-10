using Microsoft.JSInterop;

namespace PokemonApp.Interop;

public class LocalThemeInterop : ILocalThemeInterop
{
    public bool IsDarkMode { get; private set; }
    private readonly IJSRuntime _javaScript;
    private const string _setThemeIdentifier = "LocalTheme.setIsDarkMode";
    private const string _isBrowserDarkModeIdentifier = "LocalTheme.isBrowserDarkMode";

    public LocalThemeInterop(IJSRuntime javaScript)
    {
        _javaScript = javaScript;
    }

    public async Task SetIsDarkMode(bool isDarkMode)
    {
        IsDarkMode = isDarkMode;
        await _javaScript.InvokeVoidAsync(_setThemeIdentifier, isDarkMode);
    }

    public async Task<bool> IsBrowserDarkMode()
    {
        return await _javaScript.InvokeAsync<bool>(_isBrowserDarkModeIdentifier);
    }
}
