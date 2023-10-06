using Microsoft.EntityFrameworkCore;
using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private MyAppDatabaseContext _dbContext;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public UnitOfWork(MyAppDatabaseContext db)
        {
            _dbContext = db;
            Category = new CategoryRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
        }


        public void save()
        {

            _dbContext.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
