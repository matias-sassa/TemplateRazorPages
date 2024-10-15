namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    //IRepository<Category> CategoryRepository { get; }
     void SaveChanges();
    Task SaveChangesAsync();
}
