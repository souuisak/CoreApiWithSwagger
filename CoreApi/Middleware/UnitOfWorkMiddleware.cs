using CoreApi.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Middleware
{
    public class UnitOfWorkMiddleware
    {
        private readonly RequestDelegate next_;

        public UnitOfWorkMiddleware(RequestDelegate next)
        {
            next_ = next;
        }

        public async Task Invoke(HttpContext context, StoreDbContext dbContext)
        {          
            await next_.Invoke(context);

            dbContext.SaveChanges();
        }
    }
}
