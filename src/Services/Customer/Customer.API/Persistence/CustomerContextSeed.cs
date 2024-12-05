using ILogger = Serilog.ILogger;
namespace Customer.API.Persistence;

public abstract class CustomerContextSeed
{
    public static async Task SeedCustomerAsync(CustomerContext context, ILogger logger)
    {
        logger.Information(context.Customers.Count().ToString());
        if (!context.Customers.Any())
        {
           
            context.AddRange(GetCustomers());
            await context.SaveChangesAsync();
            logger.Information("Seed database associated with context {DbContextName}", nameof(CustomerContext));
        }
    }

    private static IEnumerable<Entities.Customer> GetCustomers()
    {
        return new List<Entities.Customer>()
        {
            new()
            {
                Id = Guid.Parse("aebdc74a-6e40-44ae-baec-baa569314c0"),
                FirstName = "Truong",
                LastName = "Cuong",
                EmailAddress = "cuongdevpro@gmail.com",
                UserName = "cuongtn",
            },
            new()
            {
                Id = Guid.Parse("ea48d514-08ae-46dc-923b-9417dc9b47a9"),
                FirstName = "Tam",
                LastName = "Duong",
                EmailAddress = "tamdtvp@gmail.com",
                UserName = "tamdt",
            }
        };
    }
}