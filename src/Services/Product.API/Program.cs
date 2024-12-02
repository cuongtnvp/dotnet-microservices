using Serilog;
using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting Product API up and running.");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    // Add custom configuration
    builder.AddAppConfiguration();

    // Add Services
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseInfrastructure();
    app.MigrateDatabase<ProductContext>((context,_) =>
    {
        ProductContextSeed.SeedProductsAsync(context,Log.Logger).Wait();
    })// Host
        .Run(); // auto migration
}
catch (Exception ex)
{
    string typeName = ex.GetType().Name;
    //Log.Error(typeName);
    if (typeName.Equals("HostAbortedException", StringComparison.Ordinal)) // Prevent log error in migrations
    {
        throw;
    }
    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shutting down Product API complete.");
    Log.CloseAndFlush();
}