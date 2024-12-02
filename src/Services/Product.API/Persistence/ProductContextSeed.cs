using Product.API.Entities;
using ILogger = Serilog.ILogger;
namespace Product.API.Persistence;

public class ProductContextSeed
{
    public static async Task SeedProductsAsync(ProductContext productContext, ILogger logger)
    {
        if (!productContext.Products.Any())
        {
            productContext.AddRange(GetCatalogProducts());
            await productContext.SaveChangesAsync();
            logger.Information($"Seeded data for Products Db associated with context {nameof(ProductContext)}.");
        }
    }

    private static IEnumerable<CatalogProduct> GetCatalogProducts()
    {
        return new List<CatalogProduct>()
        {
            new()
            {
                No = "Lotus",
                Name = "Esprit",
                Summary = "random text",
                Description = "random text",
                Price = (decimal)177940.49,
            },
            new()
            {
                No = "HalfLife",
                Name = "HL",
                Summary = "random text",
                Description = "random text",
                Price = (decimal)88888.89,
            }
        };
    }
}