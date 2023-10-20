using Microsoft.IdentityModel.Tokens;
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
    public class OrderHeadRepository : Repository<OrderHead>, IOrderHead
    {
        private readonly MyAppDatabaseContext _db;
        public OrderHeadRepository(MyAppDatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHead orderHead)
        {
            _db.Update(orderHead);
        }

        public void UpdateOrderStatus(int id, string OrderStatus, string? PaymentStatus = null)
        {
            OrderHead head = _db.OrderHeads.FirstOrDefault(x => x.Id == id);
            if (head != null)
            {
                head.OrderStatus = OrderStatus;
                if (!head.PaymentStatus.IsNullOrEmpty())
                {
                    head.PaymentStatus = PaymentStatus;
                }
            }
        }

        public void UpdatePaymentId(int id, string PaymentIntentId, string PaymentSessionId)
        {
            var head = _db.OrderHeads.FirstOrDefault(x => x.Id == id);
            if (head != null)
            {

                if (!PaymentSessionId.IsNullOrEmpty())
                {
                    head.PaymentSessionId = PaymentSessionId;

                }
                if (!PaymentIntentId.IsNullOrEmpty())
                {
                    //when payment successful
                    head.PaymentIntentId = PaymentIntentId;
                    head.PaymentDate = DateTime.Now;
                }
            }
        }
    }
}
