using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace Shared.Dtos.Customers;

public class CustomerDto
{
    public Guid Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string LastName { get; set; } = string.Empty;

    [Required] [EmailAddress] [Column(TypeName = "varchar(100)")]public string EmailAddress { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
   
}