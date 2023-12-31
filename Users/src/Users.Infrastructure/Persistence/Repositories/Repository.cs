using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Interfaces;
using Users.Domain.Common;
using Users.Infrastructure.Persistence.Contexts;

namespace Users.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public long Count(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (filter != null)
            query = query.Where(filter);

        return query.Count();
    }

    public async Task<long> CountAsync(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (filter != null)
            query = query.Where(filter);

        return await query.CountAsync(cancellationToken);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void DeleteById(string? id)
    {
        T? entity = GetById(id);

        if (entity is null)
            throw new NullReferenceException($"{typeof(T).Name} not found with id: {id}.");

        Delete(entity);
    }

    public async Task DeleteByIdAsync(
        string? id,
        CancellationToken cancellationToken = default)
    {
        T? entity = await GetByIdAsync(id, cancellationToken);

        if (entity is null)
            throw new NullReferenceException($"{typeof(T).Name} not found with id: {id}.");

        await DeleteAsync(entity, cancellationToken);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            entity.IsActive = false;

        UpdateRange(entities);
    }

    public async Task DeleteRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            entity.IsActive = false;

        await UpdateRangeAsync(entities, cancellationToken);
    }

    public bool Exists(Expression<Func<T, bool>> filter)
    {
        return GetQuery(filter).Any();
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await GetQuery(filter).AnyAsync(cancellationToken);
    }

    public T? GetById(string? id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public async Task<T?> GetByIdAsync(
        string? id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync(new[] { id }, cancellationToken);
    }

    public T? GetFirstOrDefault(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool disabledTracking = true,
        string[]? includedProperties = null)
    {
        var query = GetQuery(filter, orderBy, null, null, disabledTracking, includedProperties);

        return query.FirstOrDefault();
    }

    public async Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool disabledTracking = true,
        string[]? includedProperties = null,
        CancellationToken cancellationToken = default
        )
    {
        var query = GetQuery(filter, orderBy, null, null, disabledTracking, includedProperties);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public IEnumerable<T> GetList(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool disabledTracking = true,
        string[]? includedProperties = null)
    {
        var query = GetQuery(filter, orderBy, skip, take, disabledTracking, includedProperties);

        return query.ToList();
    }

    public async Task<IEnumerable<T>> GetListAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool disabledTracking = true,
        string[]? includedProperties = null,
        CancellationToken cancellationToken = default)
    {
        var query = GetQuery(filter, orderBy, skip, take, disabledTracking, includedProperties);

        return await query.ToListAsync(cancellationToken);
    }

    public void Insert(T entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
    }

    public async Task InsertAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void InsertRange(IEnumerable<T> entities)
    {
        _dbContext.AddRange(entities);
        _dbContext.SaveChanges();
    }

    public async Task InsertRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        _dbContext.AddRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
    }

    public async Task UpdateAsync(
        T entity,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            Update(entity);
    }

    public async Task UpdateRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            await UpdateAsync(entity, cancellationToken);
    }

    private IQueryable<T> GetQuery(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        bool disabledTracking = true,
        string[]? includedProperties = null)
    {
        IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (includedProperties is not null)
            foreach (var includeProperty in includedProperties)
                query = query.Include(includeProperty);

        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue && take.HasValue)
            query = query.Skip(skip.Value).Take(take.Value);

        if (disabledTracking)
            query = query.AsNoTracking();

        return query;
    }
}