using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> GetAll();
    Task<Product?> Find(Guid Id);
    IQueryable<Product> GetAllWithLastPrice();
}
