using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Configuration;
using CoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Reposirories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private StoreDbContext _dbContext { get; }
        public IRepository<Author> AuthorRepository => new Repository<Author>(_dbContext);
        public IRepository<Book> BookRepository => new Repository<Book>(_dbContext);

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        //public void Rollback() { 
        //_dbContext.
        //}
    }
}
