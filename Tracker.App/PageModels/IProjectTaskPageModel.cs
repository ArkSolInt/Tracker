using CommunityToolkit.Mvvm.Input;
using Tracker.App.Models;

namespace Tracker.App.PageModels;

public interface IProjectTaskPageModel
{
	IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
	bool IsBusy { get; }
}