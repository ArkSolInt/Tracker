using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tracker.Core.Interfaces;
using Tracker.Services;
using Tracker.Services.Interfaces;
using Tracker.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add httpclient service for API calls
builder.Services.AddHttpClient<ApiHttpClient>(client =>
    client.BaseAddress = new Uri(builder.Configuration["Backend:BaseUrl"] ?? throw new InvalidOperationException("API base URL is not configured")));

// Add device-specific services 
builder.Services.AddSingleton<IFormFactor, FormFactorService>();
builder.Services.AddScoped<IWeatherService, ClientWeatherService>();


await builder.Build().RunAsync();
