using Tracker.Core.Interfaces;

namespace Tracker.Web.Services;

public class FormFactorService : IFormFactor
{
    public string GetFormFactor()
    {
        // Implement your logic to determine the form factor
        return "Web";
    }

    public string GetPlatform()
    {
        // Implement your logic to determine the platform
        return Environment.OSVersion.ToString();
    }
}
