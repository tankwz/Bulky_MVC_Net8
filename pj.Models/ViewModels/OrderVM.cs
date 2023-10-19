using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models.ViewModels
{
    public class OrderVM
    {
        OrderHead orderHead { get; set; }
        IEnumerable<OrderDetail> orderDetail { get; set; }
    }
}
