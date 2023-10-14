using pj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartRepository>
    {
      void update(ShoppingCart cart);
       
    }
}
