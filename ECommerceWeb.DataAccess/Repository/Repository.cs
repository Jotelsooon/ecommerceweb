using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
                                  string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null) query = query.Where(filter);
        if (!string.IsNullOrEmpty(includeProperties))
            foreach (var prop in includeProperties.Split(',',
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(prop);
        return query.ToList();
    }

    public T? Get(Expression<Func<T, bool>> filter,
                  string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        if (!string.IsNullOrEmpty(includeProperties))
            foreach (var prop in includeProperties.Split(',',
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(prop);
        return query.FirstOrDefault();
    }

    public void Add(T entity) => _db.Add(entity);
    public void Remove(T entity) => _db.Remove(entity);
    public void RemoveRange(IEnumerable<T> entities) => _db.RemoveRange(entities);
}