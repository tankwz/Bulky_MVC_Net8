using pj.DataAccess.Data;
using pj.DataAccess.Repository.IRepository;
using pj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        private MyAppDatabaseContext _db;
        public CategoryRepository(MyAppDatabaseContext db) : base(db)
        {
            _db = db;
        }


     

        public void Update(Category category)
        {
            _db.Categories.Update(category);
          //  throw new NotImplementedException();
        }
    }
}
