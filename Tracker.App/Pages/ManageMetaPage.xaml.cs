namespace Tracker.App.Pages;

public partial class ManageMetaPage : ProtectedContentPage
{
	public ManageMetaPage(ManageMetaPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}