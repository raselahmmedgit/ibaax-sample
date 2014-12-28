using System;
using System.Linq;
using System.Linq.Expressions;

namespace RnD.iBaaxWebApi.Models
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(int id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        int Save();
    }
}