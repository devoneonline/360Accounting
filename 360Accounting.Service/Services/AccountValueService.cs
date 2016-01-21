using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class AccountValueService : IAccountValueService
    {
        private IAccountValueRepository repository;

        public AccountValueService(IAccountValueRepository repo)
        {
            this.repository = repo;
        }

        public AccountValue GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<AccountValue> GetAll()
        {
            return this.GetAll();
        }

        public string Insert(AccountValue entity)
        {
            return this.Insert(entity);
        }

        public string Update(AccountValue entity)
        {
            return this.Update(entity);
        }

        public void Delete(string id)
        {
            this.Delete(id);
        }

        public int Count()
        {
            return this.Count();
        }
    }
}
