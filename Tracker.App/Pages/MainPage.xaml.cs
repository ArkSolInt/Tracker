using Tracker.App.Models;
using Tracker.App.PageModels;

namespace Tracker.App.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}