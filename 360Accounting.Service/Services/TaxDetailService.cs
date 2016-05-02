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
    public class TaxDetailService : ITaxDetailService
    {
        private ITaxDetailRepository repository;

        public TaxDetailService(ITaxDetailRepository taxDetailRepository)
        {
            this.repository = taxDetailRepository;
        }

        public IList<TaxDetail> GetAll(long companyId, long taxId)
        {
            return this.repository.GetAll(companyId, taxId);
        }

        public IEnumerable<TaxDetail> GetByCodeCombinitionId(long codeCombinitionId)
        {
            return this.repository.GetByCodeCombinitionId(codeCombinitionId);
        }

        public TaxDetail GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<TaxDetail> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(TaxDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(TaxDetail entity)
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
