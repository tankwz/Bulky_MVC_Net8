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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly MyAppDatabaseContext _dbContext;
        public CompanyRepository(MyAppDatabaseContext db) :base(db) 
        {
            _dbContext  = db;
        }

        public void Update(Company company)
        {
            _dbContext.Update(company);
        }
    }
}
