using System;
using System.Linq;
using System.Linq.Expressions;

namespace lab.ngsample.Models.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(int id);
    }
}