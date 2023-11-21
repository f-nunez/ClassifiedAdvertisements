using System.Linq.Expressions;
using Users.Domain.Common;

namespace Users.Application.Common.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    long Count(Expression<Func<T, bool>>? filter = null);
    Task<long> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    void Delete(T entity);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    void DeleteById(string? id);
    Task DeleteByIdAsync(string? id, CancellationToken cancellationToken = default);
    void DeleteRange(IEnumerable<T> entities);
    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    bool Exists(Expression<Func<T, bool>> filter);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    T? GetById(string? id);
    Task<T?> GetByIdAsync(string? id, CancellationToken cancellationToken = default);
    T? GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool disabledTracking = true, string[]? includedProperties = null);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool disabledTracking = true, string[]? includedProperties = null, CancellationToken cancellationToken = default);
    IEnumerable<T> GetList(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? skip = null, int? take = null, bool disabledTracking = true, string[]? includedProperties = null);
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? skip = null, int? take = null, bool disabledTracking = true, string[]? includedProperties = null, CancellationToken cancellationToken = default);
    void Insert(T entity);
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
    void InsertRange(IEnumerable<T> entities);
    Task InsertRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<T> entities);
    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}