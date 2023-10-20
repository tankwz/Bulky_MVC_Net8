using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models.ViewModels
{
    public class OrderVM
    {
        public OrderHead orderHead { get; set; }
        public IEnumerable<OrderDetail> orderDetail { get; set; }
    }
}
