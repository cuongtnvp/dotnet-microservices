namespace Shared.Dtos;

public class UpdateProductDto
{
   public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}