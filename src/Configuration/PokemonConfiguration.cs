using PokemonApp.Services;

namespace PokemonApp.Configuration;

public static class PokemonConfiguration
{
    private const string ConfigurationAPIKey = "PokeApi:BaseUrl";

    public static void AddPokemonConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        Uri APIClient = new Uri(uriString: configuration[ConfigurationAPIKey] ?? throw new InvalidOperationException($"{ConfigurationAPIKey} is missing from configuration"));
        services.AddScoped<IPokemonService, PokemonService>();
        services.AddHttpClient<IPokemonService, PokemonService>(client =>
        {
            client.BaseAddress = APIClient;
        });
    }
}
