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
    public class AppUserRepository : Repository<AppUser>, IAppUser
    {
        private readonly MyAppDatabaseContext _db;
        public AppUserRepository( MyAppDatabaseContext db ) : base( db ) 
        {
            _db= db;
        }
    }
}
