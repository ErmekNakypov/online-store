using System.Linq.Expressions;
using Abstraction.Interfaces.Repositories;
using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Model.Common;
using Model.Entities;

namespace DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    protected DbSet<T> Set => _context.Set<T>();
    
    public IQueryable<T> GetItems(Expression<Func<T, bool>>? filter = null)
    {
        if (filter != null)
        {
            return Set.Where(filter).AsNoTracking();
        }
        return Set.AsNoTracking();
    }

    public IQueryable<T> GetTrackedItems(Expression<Func<T, bool>> filter)
    {
        return Set.Where(filter);
    }

    public async Task<T?> GetItemAsync(Expression<Func<T, bool>> filter)
    {
        return await Set.AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public async Task<T?> GetTrackedItemAsync(Expression<Func<T, bool>> filter)
    {
        return await Set.FirstOrDefaultAsync(filter);
    }                   

    public async Task<T> AddAsync(T item)
    {
        await Set.AddAsync(item);
        return item;
    }

    public T UpdateItemAsync(T item)
    {
        Set.Update(item);
        return item;
    }

    public  IQueryable<T> DeleteItem(Expression<Func<T, bool>> filter)
    {
        Set.Remove(Set.FirstOrDefault(filter));
        return GetItems();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}