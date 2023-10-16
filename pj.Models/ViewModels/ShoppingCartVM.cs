using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCarts { get; set; }

        public OrderHead OrderHead { get; set; }
        public double total { get; set; }
    }
}
