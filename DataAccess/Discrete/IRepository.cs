
using Data.Entities.Base;
using System.Linq.Expressions;

namespace DataAccess.Discrete
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(IEnumerable<T> entitites);
        Task AddAsync(T entity);
        void Update(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(IEnumerable<T> entities, bool isSoftDelete = false);
        void Delete(T entity, bool isSoftDelete = false);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<T, bool>> expression);
    }
}
