using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Tracker.Core.Models;
using Tracker.Services;
using Tracker.Services.Interfaces;
using static System.Net.WebRequestMethods;

namespace Tracker.Web.Client.Services;

public class ClientWeatherService(ApiHttpClient http, ILogger<ClientWeatherService> logger) : IWeatherService
{
    public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
    {

        try
        {
            logger.LogInformation("HttpClient is properly initialized.");
            var data = await http.GetWeatherForecastAsync();

            return data ?? [];
        }
        catch (HttpRequestException ex)
        {
            //logger.LogError(ex, "Unable to load weather data from {WeatherUrl}.", options.Value.WeatherPath);
            throw new InvalidOperationException("Unable to load weather data right now.", ex);
        }
    }
}
