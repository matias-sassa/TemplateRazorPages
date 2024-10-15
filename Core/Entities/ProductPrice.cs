using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class ProductPrice : BaseEntity
{
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public Product Product { get; set; }
}
