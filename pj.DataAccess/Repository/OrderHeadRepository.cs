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
            _db=db;
        }

        public void Update(OrderHead orderHead)
        {
            _db.Update(orderHead);
        }
    }
}
