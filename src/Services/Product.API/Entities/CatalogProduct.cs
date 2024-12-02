using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Product.API.Entities;

public class CatalogProduct : EntityAuditBase<Guid>
{
    [Required]
    [Column(TypeName = "varchar(150)")]
    public string No { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(250)")]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(255)")] public string Summary { get; set; } = string.Empty;
    [Column(TypeName = "nvarchar(250)")] public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(12,2)")] public decimal Price { get; set; }

    //Todo: StockInventory  -> inventory service calculate -> update this field (not service Product.API do)
}