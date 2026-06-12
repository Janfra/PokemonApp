using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Primitives;

namespace PokemonApp.Endpoints;

public static class ThemeEndpoints
{
    public static void MapThemeEndpoints(this IEndpointRouteBuilder builder)
    {
        var themeGroup = builder.MapGroup("api/v1/themes");
        themeGroup.MapGet("/{theme}", GetTheme);
    }

    private static RedirectHttpResult GetTheme(string theme, HttpContext context)
    {
        var options = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddMonths(1),
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/"
        };

        context.Response.Cookies.Append("theme-preference", theme, options);

        string url = "/";
        if (context.Request.Headers.TryGetValue("Referer", out StringValues value))
        {
            url = value.ToString();
        }
        return TypedResults.Redirect(url);
    }
}
