using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public abstract class CreateOrUpdateProductDto
{
    [Required]
    [MaxLength(250, ErrorMessage = "Name cannot be longer than 250")]
    public string Name { get; set; } = string.Empty;
    [MaxLength(255, ErrorMessage = "Description cannot be longer than 255")]
    public string Description { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public decimal Price { get; set; }
}