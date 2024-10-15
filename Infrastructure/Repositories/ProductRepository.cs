using AltaP.Infrastructure.Data;
using AltaP.Infrastructure.Repositories;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Product> GetAll()
    {
        return _context.Products.Where(x => !x.IsDelete);
    }    

    public async Task<Product?> Find(Guid Id)
    {
        var product = _context.Products.Include(x => x.ProductPrices)
                                       .Where(x => x.Id == Id
                                                && !x.IsDelete);

        product = FilterLastPrice(product);

        return await product.FirstOrDefaultAsync();
    }
    
    public IQueryable<Product> GetAllWithLastPrice()
    {
        var products = _context.Products
                       .Include(x => x.ProductPrices)
                       .Where(x => !x.IsDelete);

        products = FilterLastPrice(products);

        return products;
    }

    private IQueryable<Product> FilterLastPrice(IQueryable<Product> products)
    {
        return products.Select(x => new Product()
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            CreatedDate = x.CreatedDate,
            ModifiedDate = x.ModifiedDate,
            IsActive = x.IsActive,
            IsDelete = x.IsDelete,
            DeletedDate = x.DeletedDate,
            ProductPrices = x.ProductPrices.OrderByDescending(p => p.Date).Take(1).ToList()
        });
    }
}
