using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Tracker.Core.Interfaces;
using Tracker.Hyb.Models;
using Tracker.Hyb.Services;
using Tracker.Services.Interfaces;

namespace Tracker.Hyb;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        //Register needed elements for authentication:
        // This is the core functionality
        builder.Services.AddAuthorizationCore();

        // Add app services
        builder.Services.AddSingleton<TokenStorage>();
        // This is our custom provider
        builder.Services.AddScoped<MauiAuthenticationStateProvider>();
        // Use our custom provider when the app needs an AuthenticationStateProvider
        builder.Services.AddScoped<AuthenticationStateProvider>(s => (MauiAuthenticationStateProvider)s.GetRequiredService<MauiAuthenticationStateProvider>());
        builder.Services.AddScoped<IAuthenticationService>(s => s.GetRequiredService<MauiAuthenticationStateProvider>());

        // Add device-specific services 
        builder.Services.AddSingleton<IFormFactor, FormFactorService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();


        return builder.Build();
	}
}
