using System.Linq.Expressions;

namespace Abstraction.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetItems(Expression<Func<T, bool>>? filter = null);
    IQueryable<T> GetTrackedItems(Expression<Func<T, bool>>? filter = null);
    Task<T?> GetItemAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetTrackedItemAsync(Expression<Func<T, bool>> filter);
    Task<T> AddAsync(T item);
    T UpdateItemAsync(T item);

    IQueryable<T> DeleteItem(Expression<Func<T, bool>> filter);
    Task SaveChangesAsync();
}