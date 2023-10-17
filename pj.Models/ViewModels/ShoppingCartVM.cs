using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IList<ShoppingCart> ListCarts { get; set; }

        public OrderHead OrderHead { get; set; }
    }
}
