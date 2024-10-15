using AltaP.Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AltaP.Infrastructure.Repositories;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _entities;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }

    public T GetById(int id)
    {
        return _entities.Find(id);
    }

    public async Task AddAsync(T entity)
    {
        _entities.Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        T entity = await GetByIdAsync(id);
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(SoftDeletableEntity entity)
    {
        entity.IsDelete = true;
        entity.DeletedDate = DateTime.Now;

        _entities.Update(entity as T);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SoftDeletableEntity>> GetAllNotDeletedAsync()
    {
        return await _entities.OfType<SoftDeletableEntity>().Where(x => !x.IsDelete).ToListAsync();
    }

    public IEnumerable<T> GetAll()
    {
        return _entities;
    }

    public IEnumerable<PropertyEntry> GetEntryProperties(T entity)
    {
        return _context.Entry(entity).Properties;
    }
}
