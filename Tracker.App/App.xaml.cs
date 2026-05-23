using Microsoft.Extensions.DependencyInjection;

namespace Tracker.App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
        //return new Window(new AppShell());

        // Resolve AppShell from DI
        var shell = IPlatformApplication.Current?.Services.GetRequiredService<AppShell>();

        //// Add secure menu to the shell
        //var menuService = IPlatformApplication.Current?.Services.GetRequiredService<MenuService>();
        //menuService?.UpdateMenu(false);

        // Return the shell as the main window
        return new Window(shell!);
	}
}