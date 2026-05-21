using System;
using System.Collections.Generic;
using System.Text;
using Tracker.Core.Models;
using Tracker.Services.Interfaces;

namespace Tracker.Api.Services;

public class WeatherService : IWeatherService
{      

    public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
    {
        Random random = new(Seed: DateTime.Now.Minute);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var forecasts = Enumerable.Range(1, 10).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = random.Next(-20, 55),
            Summary = summaries[random.Next(summaries.Length)]
        }).ToArray();

        return forecasts;
    }
}
