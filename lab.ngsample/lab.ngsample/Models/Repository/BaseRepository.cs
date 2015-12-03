using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> All
        {
            get
            {
                return context.Set<T>().AsQueryable();
            }
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        public virtual T Find(int id)
        {
            return context.Set<T>().Find(id);
        }
    }
}