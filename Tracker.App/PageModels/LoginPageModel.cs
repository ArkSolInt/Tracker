using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tracker.App.Models;
using Tracker.Core.Models;
using Tracker.Services.Interfaces;

namespace Tracker.App.PageModels;

public partial class LoginPageModel : ObservableObject
{
	private bool _isNavigatedTo;
	private bool _dataLoaded;
	private readonly ModalErrorHandler _errorHandler;

	[ObservableProperty]
	bool _isBusy;

	[ObservableProperty]
	bool _isRefreshing;

	[ObservableProperty]
	private string _today = DateTime.Now.ToString("dddd, MMM d");


	[ObservableProperty]
	private string _email =  string.Empty;

	[ObservableProperty]
	private string _password = string.Empty;

	[ObservableProperty]
	private bool _rememberMe;

	[ObservableProperty]
	private string _message = string.Empty;

	[ObservableProperty]
    bool _isMessageVisible;

    private readonly IAuthenticationService _authenticationService;
    private readonly MenuService menuService;

    public LoginPageModel(IAuthenticationService authenticationService, MenuService menuService, ModalErrorHandler errorHandler)
	{
		_authenticationService = authenticationService;
		this.menuService = menuService;
        _errorHandler = errorHandler;
	}

	private async Task LoadData()
	{
		try
		{
			IsBusy = true;

			
		}
		finally
		{
			IsBusy = false;
		}
	}

	private async Task InitData(SeedDataService seedDataService)
	{
		await Refresh();
	}

	[RelayCommand]
	private async Task Refresh()
	{
		try
		{
			IsRefreshing = true;
			await LoadData();
		}
		catch (Exception e)
		{
			_errorHandler.HandleError(e);
		}
		finally
		{
			IsRefreshing = false;
		}
	}

	[RelayCommand]
	private void NavigatedTo() =>
		_isNavigatedTo = true;

	[RelayCommand]
	private void NavigatedFrom() =>
		_isNavigatedTo = false;

	[RelayCommand]
	private async Task Appearing()
	{
		if (!_dataLoaded)
		{
			_dataLoaded = true;
			await Refresh();
		}
		// This means we are being navigated to
		else if (!_isNavigatedTo)
		{
			await Refresh();
		}
	}

	[RelayCommand]
	private async Task Login()
	{
        var loginRequest = new LoginRequest
        {
            Email = Email?.Trim() ?? string.Empty,
            Password = Password ?? string.Empty,
            RememberMe = RememberMe,
        };

        var result = await _authenticationService.AuthenticateAsync(loginRequest);

        //menuService.UpdateMenu(result.Succeeded);

        if (!result.Succeeded)
        {
            Message = result.ErrorMessage ?? _authenticationService.LoginFailureMessage;
            IsMessageVisible = true;
            //await RefreshUiAsync();
            return;
        }

        Password = string.Empty;
		//await Navigation.PopToRootAsync();

		await Shell.Current.GoToAsync("//home");

        
    }

	[RelayCommand]
	async Task Register()
	{
		await Shell.Current.GoToAsync("//register");
    }

}