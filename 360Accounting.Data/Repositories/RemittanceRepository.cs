using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Data.Repositories
{
    public class RemittanceRepository : Repository, IRemittanceRepository
    {
        public Remittance GetSingle(string id, long companyId)
        {
            Remittance remittance = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            return remittance;
        }

        public IEnumerable<Remittance> GetAll(long companyId)
        {
            IEnumerable<Remittance> remittanceList = this.Context.Remittances;
            return remittanceList;
        }

        public string Insert(Remittance entity)
        {
            this.Context.Remittances.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Remittance entity)
        {
            var originalEntity = this.Context.Remittances.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Remittances.Remove(this.GetSingle(id, companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            return this.GetAll(companyId).Count();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}
