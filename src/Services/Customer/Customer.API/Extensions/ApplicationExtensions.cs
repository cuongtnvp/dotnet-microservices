using Customer.API.Middlewares;
using Customer.API.Controller;
namespace Customer.API.Extensions;

public static class ApplicationExtensions
{
    
    public static void UseInfrastructure(this WebApplication app)
    {
       
        app.MapCustomerApi();
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
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<ModelValidationMiddleware>();
    }
}