using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {



        private readonly MyAppDatabaseContext _db;
        public ProductRepository(MyAppDatabaseContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(Product product)
        {
            _db.Update(product); 
           // throw new NotImplementedException();
        }
    }
}
