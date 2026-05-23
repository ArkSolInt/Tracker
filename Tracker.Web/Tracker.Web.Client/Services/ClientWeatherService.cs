using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Tracker.Core.Models;
using Tracker.Services;
using Tracker.Services.Interfaces;
using static System.Net.WebRequestMethods;

namespace Tracker.Web.Client.Services;

public class ClientWeatherService(ApiHttpClient http, ILogger<ClientWeatherService> logger)
    : IWeatherService
{
    public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
    {
        try
        {
            var forecasts = await http.GetWeatherForecastsAsync();
            return forecasts ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load weather forecasts from ApiHttpClient.");

            throw new InvalidOperationException(
                "Unable to load weather information at the moment.",
                ex);
        }
    }

}