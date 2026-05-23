using Tracker.Services.Interfaces;

namespace Tracker.App.Pages;

public partial class WeatherPage : ProtectedContentPage
{
	public WeatherPage(WeatherPageModel model) 
    {
		InitializeComponent();
		BindingContext = model;
    }
}