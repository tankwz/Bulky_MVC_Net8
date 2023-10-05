using Microsoft.EntityFrameworkCore;
using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace pj.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyAppDatabaseContext _dbContext;
        internal DbSet<T> dbSet;
        public Repository(MyAppDatabaseContext db)
        {
            _dbContext = db;
            dbSet=_dbContext.Set<T>();
        }


        public void Add(T item)
        {
            dbSet.Add(item);    
          //  throw new NotImplementedException();
        }

        public T Get1(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
            //throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
            //throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
          //  throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> item)
        {
            dbSet.RemoveRange(item);
            //throw new NotImplementedException();
        }
    }
}
