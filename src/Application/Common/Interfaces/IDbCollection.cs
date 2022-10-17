using System.Linq.Expressions;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IDbCollection<T> where T : BaseAuditableEntity
{
    Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);

    Task<T?> FindByIdAsync(string id);

    Task<T?> FindOneAsync(Expression<Func<T, bool>> filter);

    Task CreateAsync(T newT);

    Task UpdateAsync(string id, T updatedT);

    Task RemoveAsync(string id);
}
