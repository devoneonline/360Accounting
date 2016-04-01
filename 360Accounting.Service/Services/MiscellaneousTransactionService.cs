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
    public class MiscellaneousTransactionService : IMiscellaneousTransactionService
    {
        private IMiscellaneousTransactionRepository repository;

        public MiscellaneousTransactionService(IMiscellaneousTransactionRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<MiscellaneousTransaction> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public MiscellaneousTransaction GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<MiscellaneousTransaction> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(MiscellaneousTransaction entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(MiscellaneousTransaction entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
