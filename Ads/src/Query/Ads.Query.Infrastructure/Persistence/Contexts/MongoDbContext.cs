using Ads.Query.Application.Common.Interfaces;
using MongoDB.Driver;

namespace Ads.Query.Infrastructure.Persistence.Contexts;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbContext(string? connectionString)
    {
        var mongoClient = new MongoClient(connectionString);

        string databaseName = mongoClient.ListDatabaseNames().First();

        _mongoDatabase = mongoClient.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>()
    {
        return _mongoDatabase.GetCollection<T>(typeof(T).Name);
    }
}