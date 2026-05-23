using Tracker.App.Models;
using Tracker.App.PageModels;

namespace Tracker.App.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}