using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        public Account GetSingle(string id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == Convert.ToInt32(id));
        }

        public IEnumerable<Account> GetAll()
        {
            return this.Context.Accounts;
        }

        public string Insert(Account entity)
        {
            throw new NotImplementedException();
        }

        public string Update(Account entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
