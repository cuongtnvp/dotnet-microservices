using System.ComponentModel.DataAnnotations;
using Customer.API.Services.Interfaces;
using Shared.Dtos.Customers;


namespace Customer.API.Extensions;

public static class MinimalApiController
{
    public static IApplicationBuilder MinimalController(this WebApplication app)
    {
        app.MapGet("/", () => "Welcome to Customer API!");
        app.MapGet("/api/customers", async (ICustomerService services) => Results.Ok( await services.GetCustomersAsync()));
        
        app.MapGet("/api/customers/{id}",   async (Guid id, ICustomerService services) => Results.Ok(await services.GetCustomerByIdAsync(id)));
        
        app.MapGet("/api/customers/by-username/{userName}",
            async (string userName, ICustomerService services) =>Results.Ok( await services.GetByUserNameAsync(userName)));
        
        app.MapPost("/api/customers", async (ICustomerService service, CreateCustomerDto customer) =>
        {
           
            
            var newCustomer = await service.CreateCustomerAsync(customer);
           return Results.Created($"/api/customers/{newCustomer.Id}", newCustomer);
        });

        app.MapPut("/api/customers/{id}", async (ICustomerService service,Guid id, UpdateCustomerDto customer) =>
        {
            var customerDto = await service.UpdateCustomerAsync(id,customer);
            return Results.Ok(customerDto);
        });

        app.MapDelete("/api/customers/{id}", async (Guid id, ICustomerService services) =>
        {

           await services.DeleteCustomerAsync(id);
           return Results.NoContent();
        });
        return app;
    }
}