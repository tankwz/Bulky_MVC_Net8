using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IAppUser AppUser { get; }
        IOrderHead OrderHead { get; }
        IOrderDetail OrderDetail { get; }
        void save();
         Task SaveAsync();

    }
}
