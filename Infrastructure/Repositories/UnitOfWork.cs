using AltaP.Infrastructure.Data;
using Core.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly IProductRepository _productRepository;

    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }
}
