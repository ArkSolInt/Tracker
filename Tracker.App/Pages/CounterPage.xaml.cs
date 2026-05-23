using Tracker.App.Models;
using Tracker.App.PageModels;

namespace Tracker.App.Pages;

public partial class CounterPage : ProtectedContentPage
{
	public CounterPage(CounterPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}