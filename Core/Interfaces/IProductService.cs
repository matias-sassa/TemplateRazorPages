using Core.Dto.Product;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetAll();
        Task<ProductDto> FindAsync(Guid Id);
        Task Create(ProductDto productDto);
        Task Update(ProductEditDto productDto);
        Task ChangeStatus(Guid id, bool isActive);
        Task Delete(Guid id);
    }
}