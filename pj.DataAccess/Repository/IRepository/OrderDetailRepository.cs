using pj.DataAccess.Data;
using pj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.DataAccess.Repository.IRepository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetail
    {
        private readonly MyAppDatabaseContext _db;
        public OrderDetailRepository(MyAppDatabaseContext db) :base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail orderDetail)
        {
            _db.Update(orderDetail);
        }

    }
}
