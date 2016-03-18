using _360Accounting.Core.Entities;
using _360Accounting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _360Accounting.Core.Interfaces;

namespace _360Accounting.Service
{
    public class RemittanceService : IRemittanceService
    {
        private IRemittanceRepository repository;

        public RemittanceService(IRemittanceRepository remittanceRepository)
        {
            this.repository = remittanceRepository;
        }

        public Remittance GetByRemitNo(string remitNo)
        {
            return this.repository.GetByRemitNo(remitNo);
        }

        public IEnumerable<Remittance> GetByRemitNo(long companyId, string remitNo)
        {
            return this.repository.GetByRemitNo(companyId, remitNo);
        }
        
        public IEnumerable<Remittance> GetAll(long companyId, long sobId, long bankId, long bankAccountId)
        {
            return this.repository.GetAll(companyId, sobId, bankId, bankAccountId);
        }

        public Remittance GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<Remittance> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Remittance entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Remittance entity)
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
