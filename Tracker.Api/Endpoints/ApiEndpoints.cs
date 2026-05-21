using Tracker.Services.Interfaces;

namespace Tracker.Api.Endpoints;

public static class ApiEndpoints
{
   
       

    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // Mapgroup for API endpoints
        var group = endpoints.MapGroup("api");

        // API endpoint for getting weather forecasts
        group.MapGet("weather", async (IWeatherService weatherService) =>
        {
            var forecasts = await weatherService.GetWeatherForecastsAsync();
            return Results.Ok(forecasts);
        });

        

        return endpoints;
    }
}
