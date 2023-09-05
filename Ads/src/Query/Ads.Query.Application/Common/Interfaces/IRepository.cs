using System.Linq.Expressions;
using Ads.Query.Domain.Common;

namespace Ads.Query.Application.Common.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    long Count(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    void Delete(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    void DeleteById(string id, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
    void DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    bool Exists(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    T? GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default);
    T GetById(string id, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    IEnumerable<T> GetList(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? skip = null, int? take = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? skip = null, int? take = null, CancellationToken cancellationToken = default);
    void Insert(T entity, CancellationToken cancellationToken = default);
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
    void InsertRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task InsertRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}