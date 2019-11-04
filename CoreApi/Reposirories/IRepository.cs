using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreApi.Reposirories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
        T Get (Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
