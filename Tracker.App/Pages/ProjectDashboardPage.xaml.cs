using Tracker.App.Models;
using Tracker.App.PageModels;

namespace Tracker.App.Pages;

public partial class ProjectDashboardPage : ProtectedContentPage
{
	public ProjectDashboardPage(ProjectDashboardPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}