using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using Tracker.App.Models;
using Tracker.App.ViewModels;
using Tracker.Core.Interfaces;
using Tracker.Services.Interfaces;

namespace Tracker.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureSyncfusionToolkit()
			.ConfigureMauiHandlers(handlers =>
			{
#if WINDOWS
				Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.AppendToMapping("KeyboardAccessibleCollectionView", (handler, view) =>
				{
					handler.PlatformView.SingleSelectionFollowsFocus = false;
				});

				Microsoft.Maui.Handlers.ContentViewHandler.Mapper.AppendToMapping(nameof(Pages.Controls.CategoryChart), (handler, view) =>
				{
					if (view is Pages.Controls.CategoryChart && handler.PlatformView is Microsoft.Maui.Platform.ContentPanel contentPanel)
					{
						contentPanel.IsTabStop = true;
					}
				});
#endif
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
				fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
			});

#if DEBUG
		builder.Logging.AddDebug();
		builder.Services.AddLogging(configure => configure.AddDebug());
#endif

        // Add app services
        builder.Services.AddSingleton<TokenStorage>();
        // This is our custom provider
        builder.Services.AddSingleton<MauiAuthenticationStateProvider>();
        // Use our custom provider when the app needs an AuthenticationStateProvider
        builder.Services.AddScoped<IAuthenticationService>(s => s.GetRequiredService<MauiAuthenticationStateProvider>());


        // Add device-specific services 
        builder.Services.AddSingleton<IFormFactor, FormFactorService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();

        // 		
        builder.Services.AddSingleton<ProjectRepository>();
		builder.Services.AddSingleton<TaskRepository>();
		builder.Services.AddSingleton<CategoryRepository>();
		builder.Services.AddSingleton<TagRepository>();
		builder.Services.AddSingleton<SeedDataService>();
		builder.Services.AddSingleton<ModalErrorHandler>();
		builder.Services.AddSingleton<MainPageModel>();
		builder.Services.AddSingleton<ProjectDashboardPageModel>();
		builder.Services.AddSingleton<ProjectListPageModel>();
		builder.Services.AddSingleton<ManageMetaPageModel>();

		builder.Services.AddTransientWithShellRoute<ProjectDetailPage, ProjectDetailPageModel>("project");
		builder.Services.AddTransientWithShellRoute<TaskDetailPage, TaskDetailPageModel>("task");

        //
        // Add view/page models
		builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<CounterPageModel>();
        builder.Services.AddSingleton<WeatherPageModel>();
		builder.Services.AddSingleton<LoginPageModel>();
		builder.Services.AddSingleton<LogoutPageModel>();

        //// Add pages
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<MenuService>();
        //builder.Services.AddTransient<MainPage>();
        //builder.Services.AddTransient<LoginPage>();
        ////builder.Services.AddTransient<RegisterPage>();
        //builder.Services.AddTransient<LogoutPage>();
        //builder.Services.AddTransient<WeatherPage>();

        return builder.Build();
	}
}
