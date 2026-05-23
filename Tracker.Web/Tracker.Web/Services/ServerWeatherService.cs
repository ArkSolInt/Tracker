using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Tracker.Core.Models;
using Tracker.Services;
using Tracker.Services.Interfaces;

namespace Tracker.Web.Services;

public class ServerWeatherService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, ILogger<ServerWeatherService> logger) : IWeatherService
{
    private HttpClient httpClient => httpClientFactory.CreateClient("BackendApi");

    public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
    {
        var httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("Unable to access the current HTTP context.");

        var accessToken = await httpContext.GetTokenAsync("access_token");
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new InvalidOperationException("Authentication token unavailable.");
        }

        CancellationToken cancellationToken = httpContext.RequestAborted;
        try
        {
            var weatherUrl = "api/weather";

            using var request = new HttpRequestMessage(HttpMethod.Get, weatherUrl);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await httpClient.SendAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                logger.LogWarning("Remote weather request returned 401 Unauthorized.");
                throw new InvalidOperationException("Your session expired. Sign in again.");
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<WeatherForecast[]>(cancellationToken: cancellationToken) ?? [];
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