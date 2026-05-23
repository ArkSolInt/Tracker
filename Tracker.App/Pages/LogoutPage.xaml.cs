using Tracker.App.Models;
using Tracker.App.PageModels;
using Tracker.Services.Interfaces;

namespace Tracker.App.Pages;

public partial class LogoutPage : ContentPage
{
	private readonly IAuthenticationService _authenticationService;
    private readonly MenuService _menuService;

    public LogoutPage(LogoutPageModel model, MenuService menuService, IAuthenticationService authenticationService)
	{
		InitializeComponent();
		BindingContext = model;
		_authenticationService = authenticationService;
		_menuService = menuService;
    }

	override async protected void OnAppearing()
	{
		base.OnAppearing();

        _authenticationService.Logout();
        //_menuService.UpdateMenu(false);
        
		await Shell.Current.GoToAsync("//home");


    }


}