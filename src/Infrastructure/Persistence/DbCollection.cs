using System.Linq.Expressions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using MongoDB.Driver;

namespace CleanArchitecture.Infrastructure.Persistence;
public class DbRepository<T> : IDbRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public DbRepository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter) =>
        await _collection.Find(filter).ToListAsync();

    public async Task<T?> FindByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<T?> FindOneAsync(Expression<Func<T, bool>> filter) =>
        await _collection.Find(filter).FirstOrDefaultAsync();

    public async Task CreateAsync(T newT) =>
        await _collection.InsertOneAsync(newT);

    public async Task UpdateAsync(string id, T updatedT) =>
        await _collection.ReplaceOneAsync(x => x.Id == id, updatedT);

    public async Task RemoveAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}
