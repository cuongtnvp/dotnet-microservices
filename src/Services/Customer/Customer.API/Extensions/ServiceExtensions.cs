using Customer.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
       services.ConfigureCustomerDbContext(configuration);
        return services;
    }

    private static IServiceCollection ConfigureCustomerDbContext(this IServiceCollection services,IConfiguration configuration)
    {
        var connectString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CustomerContext>(o =>
        {
            o.UseNpgsql(connectString);
        });
        return services;
    }
}