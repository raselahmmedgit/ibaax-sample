using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using RnD.iBaaxData;

namespace RnD.iBaaxWebApi.Models
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext context;

        private readonly IDbSet<T> _iDbSet;

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

        public void Insert(T entity)
        {
            // New entity
            _iDbSet.Add(entity);
        }

        public void Update(T entity)
        {
            // Existing entity
            _iDbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _iDbSet.Remove(entity);
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}