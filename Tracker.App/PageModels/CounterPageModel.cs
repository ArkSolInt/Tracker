using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tracker.App.Models;

namespace Tracker.App.PageModels;

public partial class CounterPageModel : ObservableObject
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
    int currentCount;

   
    public CounterPageModel(ModalErrorHandler errorHandler)
	{
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
    private void IncrementCount()
    {
        CurrentCount++;
    }


}