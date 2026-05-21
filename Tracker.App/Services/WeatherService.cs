using System;
using System.Collections.Generic;
using System.Text;
using Tracker.Core.Models;
using Tracker.Services.Interfaces;

namespace Tracker.App.Services;

public class WeatherService : IWeatherService
{      

    public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
    {
        throw new NotImplementedException();
    }
}
