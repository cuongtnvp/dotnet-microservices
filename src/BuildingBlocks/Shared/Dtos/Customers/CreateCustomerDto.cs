using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Dtos.Customers;

public class CreateCustomerDto:CreateOrUpdateCustomerDto
{
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string UserName { get; set; } = string.Empty;
}