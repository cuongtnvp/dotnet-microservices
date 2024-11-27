using System.Linq.Expressions;
using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Common;

public class RepositoryBaseAsync<T, K, TContext>(TContext context, IUnitOfWork<TContext> unitOfWork)
    : IRepositoryBaseAsync<T, K, TContext>
    where T : EntityBase<K>
    where TContext : DbContext
{
    private readonly TContext _context = context;
    private readonly IUnitOfWork<TContext> _unitOfWork = unitOfWork;

    public IQueryable<T> FindAll(bool trackChanges = false)
    {
        return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
      var items = FindAll(trackChanges);
      items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
      return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges = false)
    {
      return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>().Where(predicate);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(predicate, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        return items;
    }

    public async Task<T?> GetByIdAsync(K id) => await  FindByCondition(x=>x.Id != null && x.Id.Equals(id),trackChanges:false).FirstOrDefaultAsync();
    

    public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties)
    {
        return await FindByCondition(x=>x.Id != null && x.Id.Equals(id),trackChanges:false, includeProperties).FirstOrDefaultAsync();
    }

    public async Task<K> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity.Id;
    }

    public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
    {
        var entityBases = entities.ToList();
        await _context.Set<T>().AddRangeAsync(entityBases);
        return entityBases.Select(entity => entity.Id).ToList();
    }

    public Task UpdateAsync(T entity)
    {
       if(_context.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;
       T exist = _context.Set<T>().Find(entity.Id)!;
       _context.Entry(exist!).CurrentValues.SetValues(entity);
       return Task.CompletedTask;
    }

    public Task UpdateListAsync(IEnumerable<T> entities)
    {
        return _context.Set<T>().AddRangeAsync(entities);
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteListAsync(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task<int> SaveChangesAsync()
    {
       return _unitOfWork.CommitAsync();
    }

    public  Task<IDbContextTransaction> BeginTransactionAsync()
    {
       return _context.Database.BeginTransactionAsync();
    }

    public async Task EndTransactionAsync()
    {
        await SaveChangesAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public Task RollbackTransactionAsync()
    {
       return _context.Database.RollbackTransactionAsync();
    }
}