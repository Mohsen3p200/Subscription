using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
   public interface IGenericRepository<T> where T:class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
                              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                              string includeProperties = null);

       Task<T> GetFirstOrDefaultAsync(
           Expression<Func<T, bool>> filter = null,
           string includeProperties = null
           );

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        Task Remove(int id);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
