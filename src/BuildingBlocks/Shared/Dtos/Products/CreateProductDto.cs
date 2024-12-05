namespace Shared.Dtos;

public class CreateProductDto:CreateOrUpdateProductDto
{
    public string No { get; set; } = string.Empty;
   
}