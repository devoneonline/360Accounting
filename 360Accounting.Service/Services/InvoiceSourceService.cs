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
    public class InvoiceSourceService : IInvoiceSourceService
    {
        private IInvoiceSourceRepository repository;

        public InvoiceSourceService(IInvoiceSourceRepository repo)
        {
            this.repository = repo;
        }

        public List<InvoiceSource> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }
        
        public InvoiceSource GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<InvoiceSource> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(InvoiceSource entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(InvoiceSource entity)
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
