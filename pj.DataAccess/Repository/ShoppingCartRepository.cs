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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly MyAppDatabaseContext _dbContext;
        public ShoppingCartRepository(MyAppDatabaseContext dbContext) : base(dbContext)
        {

            _dbContext = dbContext;

        }

        public void Update(ShoppingCart cart)
        {
            _dbContext.ShoppingCarts.Update(cart);

        }

    
    }
}
