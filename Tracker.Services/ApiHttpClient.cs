using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Tracker.Core.Models;

namespace Tracker.Services
{
    public class ApiHttpClient(HttpClient http, ILogger<ApiHttpClient> logger)
    {
        public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
        {
            const string weatherUri = "api/weather";

            try
            {
                var result = await http.GetFromJsonAsync<WeatherForecast[]>(weatherUri);

                if (result is null)
                {
                    logger.LogWarning("Weather API returned null data");
                    throw new InvalidOperationException("Weather API returned null data");
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex,
                    "Network error while calling Weather API. URL: {Url}", weatherUri);

                throw new InvalidOperationException(
                    "Unable to reach the Weather API. Check network or server availability.",
                    ex);
            }
            catch (NotSupportedException ex)
            {
                logger.LogError(ex,
                    "Unsupported content type returned by Weather API");

                throw new InvalidOperationException(
                    "Weather API returned data in an unsupported format.",
                    ex);
            }
            catch (JsonException ex)
            {
                logger.LogError(ex,
                    "JSON parsing error while reading Weather API response");

                throw new InvalidOperationException(
                    "Weather API returned invalid JSON.",
                    ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Unexpected error occurred while retrieving weather data");

                throw new InvalidOperationException(
                    "An unexpected error occurred while retrieving weather data.",
                    ex);
            }
        }
    }

}
