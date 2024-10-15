using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Product : SoftDeletableEntity
{
    [StringLength(500)]
    public required string Name { get; set; }
    [StringLength(55)]
    public string? Code { get; set; }
    [Required]
    public bool IsActive { get; set; } = true;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public List<ProductPrice> ProductPrices { get; set; }
}
