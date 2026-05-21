using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Tracker.Core.Models;
using Tracker.Services.Interfaces;

namespace Tracker.Hyb.Services;

public class WeatherService : IWeatherService
{      
    public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
    {
        var forecasts = Array.Empty<WeatherForecast>();
        try
        {
            var httpClient = new HttpClient();
            var weatherUrl = "https://localhost:7178/api/weather";
            
            forecasts = (await httpClient.GetFromJsonAsync<WeatherForecast[]>(weatherUrl)) ?? [];
        }
        catch (HttpRequestException httpEx)
        {
            Debug.WriteLine($"HTTP Request error: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }

        return forecasts;
    }
}
