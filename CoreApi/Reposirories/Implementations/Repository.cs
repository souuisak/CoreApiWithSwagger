using CoreApi.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreApi.Reposirories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StoreDbContext _dbContext;
        private DbSet<T> _dbSet => _dbContext.Set<T>();
        public Repository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<T> SetIncludedItems(DbSet<T> _dbSet, params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = _dbSet.AsQueryable();

            if (includeExpressions.Any())
                set = includeExpressions
                      .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                        (_dbSet, (current, expression) => current.Include(expression));
            return set;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            T existing = _dbSet.Find(entity);
            if (existing != null) _dbContext.Set<T>().Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = SetIncludedItems(_dbSet, includeExpressions);

            return set.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = SetIncludedItems(_dbSet, includeExpressions);

            return set.AsEnumerable();
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = SetIncludedItems(_dbSet, includeExpressions);

            return set.Where(predicate).AsEnumerable();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbSet.Attach(entity);
        }
    }
}
