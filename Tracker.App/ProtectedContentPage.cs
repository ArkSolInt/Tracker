using Tracker.Services.Interfaces;

namespace Tracker.App;

[ContentProperty(nameof(Content))]
public partial class ProtectedContentPage: ContentPage
{
    private readonly IAuthenticationService authGuard;

    public ProtectedContentPage()
    {
        this.authGuard = IPlatformApplication.Current.Services.GetRequiredService<IAuthenticationService>();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Check if the user is authenticated
        if (!await authGuard.IsAuthenticatedAsync())
        {
            // If not authenticated, navigate to the login page
            await Shell.Current.GoToAsync("//login");

            return;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}