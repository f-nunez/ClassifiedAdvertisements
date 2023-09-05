using MongoDB.Driver;

namespace Ads.Query.Application.Common.Interfaces;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>();
}