using Contracts.Common.Interfaces;
using Customer.API.AutoMapper;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
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

        services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));


        services.AddScoped(typeof(IRepositoryQueryBase<,,>), typeof(RepositoryBaseAsync<,,>))
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<ICustomerService, CustomerService>();

        return services;
    }

    private static IServiceCollection ConfigureCustomerDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CustomerContext>(o => { o.UseNpgsql(connectString); });
        return services;
    }
}