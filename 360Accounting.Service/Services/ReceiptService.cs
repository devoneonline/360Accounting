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
    public class ReceiptService : IReceiptService
    {
        private IReceiptRepository repository;

        public ReceiptService(IReceiptRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<Receipt> GetReceipts(string sobId, string periodId, long companyId)
        {
            return this.repository.GetReceipts(sobId, periodId, companyId);
        }

        public Receipt GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<Receipt> GetAll( long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Receipt entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Receipt entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
