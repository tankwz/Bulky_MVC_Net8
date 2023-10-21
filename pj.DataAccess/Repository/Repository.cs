using Microsoft.EntityFrameworkCore;
using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


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
            _dbContext.Products.Include(u => u.Category).Include(u=>u.CategoryID);
        }


        public void Add(T item)
        {

            dbSet.AddAsync(item);    
        }
        public async Task AddAsync(T item)
        {
             await dbSet.AddAsync(item);
        }

        public T Get1(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                 query = dbSet;
            }
            else
            {
                query  = dbSet.AsNoTracking();

            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public async Task<T> Get1Async(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();

            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();

        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter,string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            
            if(filter != null)
            {
               query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
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
