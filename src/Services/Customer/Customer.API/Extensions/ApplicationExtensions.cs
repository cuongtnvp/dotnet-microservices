using Customer.API.Services.Interfaces;

namespace Customer.API.Extensions;

public static class ApplicationExtensions
{
    public static void UseInfrastructure(this WebApplication app)
    {
        app.MapGet("/", () => "Welcome to Customer API!");
        app.MapGet("/api/customers", async (ICustomerService services) => await services.GetCustomersAsync());
        app.MapGet("/api/customers/{userName}",
            async (string userName, ICustomerService services) => await services.GetByUserNameAsync(userName));

        // app.MapPost("/",()=>"Welcome to Customer API!");
        // app.MapPut("/",()=>"Welcome to Customer API!");
        // app.MapDelete("/",()=>"Welcome to Customer API!");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        
        // app.UseHttpsRedirection(); Only for section 12
        app.UseAuthorization();
        app.MapControllers();
    }
}