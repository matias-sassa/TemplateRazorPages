namespace Core.Dto.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
