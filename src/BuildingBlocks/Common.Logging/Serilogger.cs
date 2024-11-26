namespace Common.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;

public static class Serilogger
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
        (context, configuration) =>
        {
            var applicationName = context.HostingEnvironment.ApplicationName?
                .ToLower().Replace(oldValue: ".", newValue: "-");

            var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";
            configuration
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty(name: "Environment", environmentName)
                .Enrich.WithProperty(name: "Application", applicationName)
                .ReadFrom.Configuration(context.Configuration);
        };
}