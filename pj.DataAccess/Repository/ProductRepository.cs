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
    public class ProductRepository : Repository<Product>, IProductRepository
    {



        private readonly MyAppDatabaseContext _db;
        public ProductRepository(MyAppDatabaseContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault( u=>u.Id == product.Id);           
            if(productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.Description = product.Description;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price = product.Price;
                productFromDb.Price50 = product.Price50;
                productFromDb.Price100 = product.Price100;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.CategoryID = product.CategoryID;
                productFromDb.Author = product.Author;
                if(productFromDb.ImageUrl != null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }
            }


//            _db.Products.Update(product); 
           // throw new NotImplementedException();
        }
    }
}
