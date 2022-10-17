using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContext : IApplicationDbContext
{
    private readonly MongoClient client;
    private readonly IMongoDatabase database;

    public IDbRepository<TodoItem> TodoItems { get; }

    public ApplicationDbContext(
        IOptions<DatabaseConfiguration> databaseConfiguration,
        IDateTime _dateTime)
    {
        client = new MongoClient(databaseConfiguration.Value.ConnectionString);
        database = client.GetDatabase(databaseConfiguration.Value.DatabaseName);
        TodoItems = new DbRepository<TodoItem>(database.GetCollection<TodoItem>(databaseConfiguration.Value.TodoItemsCollectionName), _dateTime);
    }

    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
        {
            cm.SetIsRootClass(true);
            cm.MapIdMember(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer().WithRepresentation(BsonType.ObjectId));
            cm.MapMember(c => c.CreatedAt);
            cm.MapMember(c => c.UpdatedAt);
            cm.SetIdMember(cm.GetMemberMap(c => c.Id));
        });
        BsonClassMap.RegisterClassMap<TodoItem>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
        });
    }
}
