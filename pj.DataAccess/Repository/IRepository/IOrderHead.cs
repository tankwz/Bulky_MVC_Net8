using pj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository.IRepository
{
    public interface IOrderHead : IRepository<OrderHead>
    {
        void Update(OrderHead orderHead);
        void UpdateOrderStatus(int id, string OrderStatus, string? PaymentStatus=null);
        void UpdatePaymentId(int id, string PaymentIntentId, string PaymentSessionId);

    }
}
