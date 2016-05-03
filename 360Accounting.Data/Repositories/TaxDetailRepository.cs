using _360Accounting.Core.Interfaces;
using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _360Accounting.Data.Repositories
{
    public class TaxDetailRepository : Repository, ITaxDetailRepository
    {
        public IList<TaxDetail> GetAll(long companyId, long taxId)
        {
            IList<TaxDetail> entityList = this.Context.TaxDetails
                .Where(x => x.TaxId == taxId).ToList();
            return entityList;
        }
        
        public TaxDetail GetSingle(string id, long companyId)
        {
            TaxDetail entity = this.GetAll(companyId)
                .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            return entity;
        }

        public IEnumerable<TaxDetail> GetAll(long companyId)
        {
            IEnumerable<TaxDetail> entityList = this.Context.TaxDetails;
            return entityList;
        }

        public IEnumerable<TaxDetail> GetByCodeCombinitionId(long codeCombinitionId)
        {
            return this.Context.TaxDetails.Where(rec => rec.CodeCombinationId == codeCombinitionId);
        }

        public string Insert(TaxDetail entity)
        {
            this.Context.TaxDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(TaxDetail entity)
        {
            var originalEntity = this.Context.TaxDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.TaxDetails.Remove(this.GetSingle(id, companyId));
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
