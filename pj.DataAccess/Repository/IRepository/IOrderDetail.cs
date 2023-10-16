using pj.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository.IRepository
{
    public interface IOrderDetail : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
