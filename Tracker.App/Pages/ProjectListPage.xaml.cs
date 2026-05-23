namespace Tracker.App.Pages;

public partial class ProjectListPage : ProtectedContentPage
{
	public ProjectListPage(ProjectListPageModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}
}