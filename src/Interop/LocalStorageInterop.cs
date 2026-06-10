using Microsoft.JSInterop;

namespace PokemonApp.Interop;

public class LocalStorageInterop : ILocalStorageInterop
{
    private readonly IJSRuntime _javaScript;
    private const string _getItemIdentifier = "LocalStorageActions.getItem";
    private const string _setItemIdentifier = "LocalStorageActions.setItem";
    private const string _removeItemIdentifier = "LocalStorageActions.removeItem";
    private const string _clearDataIdentifier = "LocalStorageActions.clearData";

    public LocalStorageInterop(IJSRuntime javaScript)
    {
        _javaScript = javaScript;
    }

    public async Task<string> GetItem(string key)
    {
        return await _javaScript.InvokeAsync<string>(_getItemIdentifier, key);
    }

    public async Task SetItem(string key, string value)
    {
        await _javaScript.InvokeVoidAsync(_setItemIdentifier, key, value);
    }

    public async Task RemoveItem(string key)
    {
        await _javaScript.InvokeVoidAsync(_removeItemIdentifier, key);
    }

    public async Task ClearData()
    {
        await _javaScript.InvokeVoidAsync(_clearDataIdentifier);
    }
}
