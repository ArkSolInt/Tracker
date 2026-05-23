using Tracker.Core.Enums;
using Tracker.Core.Models;

namespace Tracker.Services.Interfaces;

public interface IAuthenticationService
{
    LoginStatus LoginStatus { get; }
    string LoginFailureMessage { get; }
    string? CurrentEmail { get; }

    Task<bool> IsAuthenticatedAsync();
    Task<AccessTokenInfo?> GetAccessTokenInfoAsync();
    Task<AuthenticationResult> AuthenticateAsync(LoginRequest loginRequest);
    Task<AuthenticationResult> RegisterAsync(RegisterRequest registerRequest);
    void Logout();
}
