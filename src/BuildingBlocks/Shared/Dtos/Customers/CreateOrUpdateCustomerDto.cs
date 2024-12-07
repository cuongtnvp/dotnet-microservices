using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Dtos.Customers;

public class CreateOrUpdateCustomerDto
{
   
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string LastName { get; set; } = string.Empty;

    [Required] [EmailAddress] [Column(TypeName = "varchar(100)")]public string EmailAddress { get; set; } = string.Empty;
    
}