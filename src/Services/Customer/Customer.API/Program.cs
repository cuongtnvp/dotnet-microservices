using Serilog;
using Common.Logging;
using Customer.API.Extensions;
using Customer.API.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);
Log.Information("Starting Customer API up and running.");

try
{
    builder.AddAppConfiguration();
// Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseInfrastructure();

    app.MigrateDatabase<CustomerContext>((context, _) =>
    {
        CustomerContextSeed.SeedCustomerAsync(context, Log.Logger).Wait();
    }).Run();
}
catch (Exception ex)
{
    string typeName = ex.GetType().Name;
    //Log.Error(typeName);
    if (typeName.Equals("HostAbortedException", StringComparison.Ordinal)) throw; // Prevent log error in migrations
    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shutting down Customer API complete.");
    Log.CloseAndFlush();
}