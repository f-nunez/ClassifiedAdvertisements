using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ads.Query.Domain.Common;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string? Id { get; set; }
    public bool IsActive { get; set; }
    public long Version { get; set; }
}