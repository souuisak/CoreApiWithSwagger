using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreApi.Reposirories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(params Expression<Func<T, object>>[] includeExpressions);
        //IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
