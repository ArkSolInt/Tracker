namespace Tracker.App.Pages;

public partial class TaskDetailPage : ProtectedContentPage
{
	public TaskDetailPage(TaskDetailPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}