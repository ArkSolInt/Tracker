using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Tracker.Core.Models;
using Tracker.Services.Interfaces;

namespace Tracker.App.Services;

public class WeatherService(MauiAuthenticationStateProvider authenticationStateProvider) : IWeatherService
{
    public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
    {
        var forecasts = Array.Empty<WeatherForecast>();
        try
        {
            var accessTokenInfo = await authenticationStateProvider.GetAccessTokenInfoAsync();

            if (accessTokenInfo is null)
            {
                throw new Exception("Could not retrieve access token to get weather forecast.");
            }

            var httpClient = HttpClientHelper.GetHttpClient();
            var weatherUrl = HttpClientHelper.WeatherUrl;

            var token = accessTokenInfo.LoginResponse.AccessToken;
            var scheme = accessTokenInfo.LoginResponse.TokenType; //"Bearer"

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(scheme))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
                forecasts = (await httpClient.GetFromJsonAsync<WeatherForecast[]>(weatherUrl)) ?? [];
            }
            else
            {
                Debug.WriteLine("Token or scheme is null or empty.");
            }
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine($"HTTP Request error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
        }

        return forecasts;
    }
}
