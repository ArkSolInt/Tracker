using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Tracker.Core.Models;

namespace Tracker.Services
{
    public class ApiHttpClient
    {
        private readonly HttpClient http;

        public ApiHttpClient(HttpClient http)
        {
            this.http = http;
        }

        public async Task<WeatherForecast[]> GetWeatherForecastAsync() =>
            await http.GetFromJsonAsync<WeatherForecast[]>("api/weather") ?? [];
    }
}
    