namespace BlazorWebApp.Endpoints;

internal sealed class BlazorRegisterForm
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
}
