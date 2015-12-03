using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(int id);
    }
}