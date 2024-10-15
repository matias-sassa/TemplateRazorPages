using Core.Entities;

namespace Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(SoftDeletableEntity entity);
    Task<IEnumerable<SoftDeletableEntity>> GetAllNotDeletedAsync();
    T GetById(int id);
    IEnumerable<T> GetAll();
}
