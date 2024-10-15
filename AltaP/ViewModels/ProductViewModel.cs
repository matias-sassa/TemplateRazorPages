using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AltaP.ViewModels;

public class ProductViewModel
{
    public ProductViewModel() {}

    public Guid Id { get; set; }
    [StringLength(500)]
    [DisplayName("Nombre")]
    public required string Name { get; set; }

    [DisplayName("Precio")]
    public decimal Price { get; set; }

    [StringLength(55)]
    [DisplayName("Codigo")]
    public string? Code { get; set; }

    [Required]
    [DisplayName("Activo")]
    public bool IsActive { get; set; } = true;

    bool IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    [DisplayName("Fecha de creación")]
    public DateTime? CreatedDate { get; set; }
    [Column(TypeName = "datetime")]
    [DisplayName("Fecha de modificación")]
    public DateTime? ModifiedDate { get; set; }
}
