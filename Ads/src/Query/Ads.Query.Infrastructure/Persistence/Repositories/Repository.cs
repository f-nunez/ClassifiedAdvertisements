using System.Linq.Expressions;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Ads.Query.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _mongoCollection;

    public Repository(IMongoDbContext mongoDbContext)
    {
        _mongoCollection = mongoDbContext.GetCollection<T>();
    }

    public long Count(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(filter);

        return _mongoCollection.CountDocuments(
            filter: filterDefinition, cancellationToken: cancellationToken);
    }

    public async Task<long> CountAsync(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(filter);

        return await _mongoCollection.CountDocumentsAsync(
            filter: filterDefinition, cancellationToken: cancellationToken);
    }

    public void Delete(T entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => x.Id == entity.Id);

        _mongoCollection.DeleteOne(filterDefinition, cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => x.Id == entity.Id);

        await _mongoCollection.DeleteOneAsync(filterDefinition, cancellationToken);
    }

    public void DeleteById(string? id, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => x.Id == id);

        _mongoCollection.DeleteOne(filterDefinition, cancellationToken);
    }

    public async Task DeleteByIdAsync(string? id, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => x.Id == id);

        await _mongoCollection.DeleteOneAsync(filterDefinition, cancellationToken);
    }

    public void DeleteRange(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => entities.Any(y => y.Id == x.Id));

        _mongoCollection.DeleteMany(filterDefinition, cancellationToken);
    }

    public async Task DeleteRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => entities.Any(y => y.Id == x.Id));

        await _mongoCollection.DeleteManyAsync(filterDefinition, cancellationToken);
    }

    public bool Exists(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(filter);

        long count = _mongoCollection.CountDocuments(
            filter: filterDefinition, cancellationToken: cancellationToken);

        return count > 0;
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(filter);

        long count = await _mongoCollection.CountDocumentsAsync(
            filter: filterDefinition, cancellationToken: cancellationToken);

        return count > 0;
    }

    public T GetById(string? id, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => x.Id == id);

        return _mongoCollection.Find(filterDefinition).FirstOrDefault(cancellationToken);
    }

    public async Task<T> GetByIdAsync(
        string? id,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(x => x.Id == id);

        return await _mongoCollection
            .Find(filterDefinition)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public T? GetFirstOrDefault(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        var query = GetQuery(filter, orderBy);

        return query.FirstOrDefault(cancellationToken);
    }

    public async Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        var query = GetQuery(filter, orderBy);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public IEnumerable<T> GetList(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default)
    {
        var query = GetQuery(filter, orderBy, skip, take);

        return query.ToList(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetListAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default)
    {
        var query = GetQuery(filter, orderBy, skip, take);

        return await query.ToListAsync(cancellationToken);
    }

    public void Insert(T entity, CancellationToken cancellationToken = default)
    {
        _mongoCollection.InsertOne(
            document: entity, cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _mongoCollection.InsertOneAsync(
            document: entity, cancellationToken: cancellationToken);
    }

    public void InsertRange(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        _mongoCollection.InsertMany(documents: entities, cancellationToken: cancellationToken);
    }

    public async Task InsertRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        await _mongoCollection.InsertManyAsync(
            documents: entities, cancellationToken: cancellationToken);
    }

    public void Update(T entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Eq(x => x.Id, entity.Id);

        _mongoCollection.ReplaceOne(
            filter: filterDefinition, replacement: entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Eq(x => x.Id, entity.Id);

        await _mongoCollection.ReplaceOneAsync(
            filter: filterDefinition, replacement: entity, cancellationToken: cancellationToken);
    }

    public void UpdateRange(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            Update(entity, cancellationToken);
    }

    public async Task UpdateRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            await UpdateAsync(entity, cancellationToken);
    }

    private IMongoQueryable<T> GetQuery(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? skip = null,
        int? take = null)
    {
        IQueryable<T> query = _mongoCollection.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue && take.HasValue)
            query = query.Skip(skip.Value).Take(take.Value);

        return (IMongoQueryable<T>)query;
    }
}