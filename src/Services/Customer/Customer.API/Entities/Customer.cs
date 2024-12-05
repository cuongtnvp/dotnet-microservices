using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Customer.API.Entities;

public class Customer : EntityBase<Guid>
{
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
}