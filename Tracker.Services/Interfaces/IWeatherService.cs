using System;
using System.Collections.Generic;
using System.Text;
using Tracker.Core.Models;

namespace Tracker.Services.Interfaces;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetWeatherForecastsAsync();
}
