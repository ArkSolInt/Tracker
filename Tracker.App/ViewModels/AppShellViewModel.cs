using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using Tracker.Services.Interfaces;

namespace Tracker.App.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        private readonly IAuthenticationService authenticationService;

        [ObservableProperty]
        private bool isAuthenticated;

        public AppShellViewModel(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
           
            UpdateAuthState();
        }

        public async void UpdateAuthState()
        {
            IsAuthenticated = await authenticationService.IsAuthenticatedAsync();            
        }
    }

}
