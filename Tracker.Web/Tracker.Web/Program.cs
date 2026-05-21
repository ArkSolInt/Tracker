using Tracker.Core.Interfaces;
using Tracker.Services;
using Tracker.Services.Interfaces;
using Tracker.Web.Components;
using Tracker.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add httpclient service for API calls
builder.Services.AddHttpClient<ApiHttpClient>(client =>
    client.BaseAddress = new Uri(builder.Configuration["Backend:BaseUrl"] ?? throw new InvalidOperationException("API base URL is not configured")));


// Add device-specific services 
builder.Services.AddSingleton<IFormFactor, FormFactorService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();



var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
    typeof(Tracker.SharedUI._Imports).Assembly, 
    typeof(Tracker.Web.Client._Imports).Assembly
    );

app.Run();
