using Tracker.App.Models;

namespace Tracker.App.Pages;

public partial class ProjectDetailPage : ProtectedContentPage
{
	public ProjectDetailPage(ProjectDetailPageModel model)
	{
		InitializeComponent();

		BindingContext = model;
	}
}
