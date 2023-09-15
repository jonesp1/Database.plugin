using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Database.plugin
{
    public interface IMongoDBRepository<T> : IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        T CreateSync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(string id);

        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task RemoveAsync(string id);
        Task UpdateAsync(T entity);
    }
}