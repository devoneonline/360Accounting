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
    public class InvoiceTypeService : IInvoiceTypeService
    {
        private IInvoiceTypeRepository repository;

        public InvoiceTypeService(IInvoiceTypeRepository repo)
        {
            this.repository = repo;
        }

        public InvoiceType GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id,companyId);
        }

        public IEnumerable<InvoiceType> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public IEnumerable<InvoiceType> GetInvoiceTypes(long sobId, long companyId)
        {
            return this.repository.GetInvoiceTypes(sobId, companyId);
        }

        public string Insert(InvoiceType entity)
        {
            if (entity.IsValid())
                return this.repository.Insert(entity);
            else
                return "Entity is not in valid state";
        }

        public string Update(InvoiceType entity)
        {
            if (entity.IsValid())
                return this.repository.Update(entity);
            else
                return "Entity is not in valid state";
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id,companyId);
        }

        public int Count( long companyId)
        {
            return this.repository.Count(companyId);
        }


        public IEnumerable<InvoiceType> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate)
        {
            return this.repository.GetAll(companyId, sobId, startDate, endDate);
        }
    }
}