namespace Core.Dto.Product;

public class ProductEditDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal? Price { get; set; }
}
