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
        public Repository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            T existing = _dbContext.Set<T>().Find(entity);
            if(existing != null ) _dbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Get(params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = includeExpressions
                  .Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                    (_dbContext.Set<T>(), (current, expression) => current.Include(expression));

            return set.AsEnumerable<T>();
        }

        //public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        //{
        //    return _dbContext.Set<T>().Where(predicate).AsEnumerable();
        //}

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Attach(entity);
        }
    }
}
