using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Product.API.Extensions;

public static class ConfigureHostExtensions
{
    public static void AddAppConfiguration(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;

        // Configure application settings
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Default app settings
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true) // Environment-specific settings
            .AddEnvironmentVariables(); // Add environment variables
    }
}