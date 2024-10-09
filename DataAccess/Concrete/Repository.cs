
using Data.Entities.Base;
using DataAccess.Discrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IQueryable<T> _entities;

        public Repository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _entities = _dbSet.Where(e => e.IsActive);
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _entities.SingleOrDefaultAsync(expression);
        }

        public async Task AddAsync(IEnumerable<T> entitites)
        {
            entitites = entitites.Select(e =>
            {
                e.IsActive = true;
                e.CreatedDate = DateTime.Now;
                return e;
            });

            await _dbSet.AddRangeAsync(entitites);
        }

        public async Task AddAsync(T entity)
        {
            entity.IsActive = true;
            entity.CreatedDate = DateTime.Now;

            await _dbSet.AddAsync(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            entities = entities.Select(e =>
            {
                e.ChangedDate = DateTime.Now;
                return e;
            });

            _dbSet.UpdateRange(entities);
        }

        public void Update(T entity)
        {
            entity.ChangedDate = DateTime.Now;

            _dbSet.Update(entity);
        }

        public void Delete(IEnumerable<T> entities, bool isSoftDelete = false)
        {
            if (isSoftDelete)
            {
                entities = entities.Select(e =>
                {
                    e.IsActive = false;
                    e.DeactivatedDate = DateTime.Now;
                    return e;
                });

                _dbSet.UpdateRange(entities);
            }
            else
            {
                _dbSet.RemoveRange(_entities);
            }
        }

        public void Delete(T entity, bool isSoftDelete = false)
        {
            if (isSoftDelete)
            {
                entity.IsActive = false;
                entity.DeactivatedDate = DateTime.Now;

                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<bool> AnyAsync()
        {
            return await _entities.AnyAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _entities.AnyAsync(expression);
        }

        public async Task<long> CountAsync()
        {
            return await _entities.LongCountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _entities.LongCountAsync(expression);
        }
    }
}
