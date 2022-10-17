using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContext : IApplicationDbContext
{
    private readonly MongoClient client;
    private readonly IMongoDatabase database;

    public IDbCollection<TodoItem> TodoItems { get; }

    public ApplicationDbContext(
        IOptions<DatabaseConfiguration> databaseConfiguration)
    {
        client = new MongoClient(databaseConfiguration.Value.ConnectionString);
        database = client.GetDatabase(databaseConfiguration.Value.DatabaseName);
        TodoItems = new DbCollection<TodoItem>(database.GetCollection<TodoItem>(databaseConfiguration.Value.TodoItemsCollectionName));
    }
}
