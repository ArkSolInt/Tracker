using Microsoft.Extensions.Logging;
using Tracker.Core.Interfaces;
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

        // Add device-specific services 
        builder.Services.AddSingleton<IFormFactor, FormFactorService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();


        return builder.Build();
	}
}
