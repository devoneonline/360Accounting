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

        public List<AccountValue> GetBySegment(string segment)
        {
            return this.repository.GetBySegment(segment);
        }
        
        public AccountValue GetSingle(string id)
        {
            return this.repository.GetSingle(id);
        }

        public IEnumerable<AccountValue> GetAll()
        {
            return this.repository.GetAll();
        }

        public string Insert(AccountValue entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(AccountValue entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id)
        {
            this.repository.Delete(id);
        }

        public int Count()
        {
            return this.repository.Count();
        }
    }
}
