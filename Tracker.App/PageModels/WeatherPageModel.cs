using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tracker.Core.Interfaces;
using Tracker.Core.Models;
using Tracker.Services.Interfaces;

namespace Tracker.App.PageModels;

public partial class WeatherPageModel : ObservableObject
{
	private bool _isNavigatedTo;
	private readonly ModalErrorHandler _errorHandler;
    private IWeatherService _weatherService;
    private bool _dataLoaded;

    [ObservableProperty]
	bool _isBusy;

	[ObservableProperty]
	bool _isRefreshing;

	[ObservableProperty]
	private string _today = DateTime.Now.ToString("dddd, MMM d");

	[ObservableProperty]
	private string _formFactor;

    [ObservableProperty]
    private List<WeatherForecast> _forecasts = [];

    public WeatherPageModel(IFormFactor formFactor, IWeatherService weatherService, ModalErrorHandler errorHandler)
	{
		_errorHandler = errorHandler;
		_formFactor = $"Welcome to your new app running on {formFactor.GetFormFactor()} using {formFactor.GetPlatform()}.";
        _weatherService = weatherService;
    }

	[RelayCommand]
    private async Task LoadData()
    {
        try
        {
            IsBusy = true;

            Forecasts = [.. (await _weatherService.GetWeatherForecastsAsync())];
        }
        finally
        {
            IsBusy = false;
            //OnPropertyChanged(nameof(HasCompletedTasks));
        }
    }

    private async Task InitData()
    {
        //bool isSeeded = Preferences.Default.ContainsKey("is_seeded");

        //if (!isSeeded)
        //{
        //    await seedDataService.LoadSeedDataAsync();
        //}

        //Preferences.Default.Set("is_seeded", true);
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
            await InitData();
            _dataLoaded = true;
            await Refresh();
        }
        // This means we are being navigated to
        else if (!_isNavigatedTo)
        {
            await Refresh();
        }
    }
}