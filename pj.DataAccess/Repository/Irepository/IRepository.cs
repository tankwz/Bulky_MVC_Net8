using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        T Get1(Expression<Func<T, bool>> filter, string? includeProperties = null,bool tracked = false);


        Task<T> Get1Async(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
    
        // Get1(Experssion<Func<T,bool>>
        //Get (Experssion<Func<t,bool>>)???????????????????????????????????
        // Exoression<Func<t,bool>> 
        void Add(T item);
        Task AddAsync(T item);


        void Remove(T item);
        void RemoveRange(IEnumerable<T> item);
    }
}
