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
    public class BuyerRepository : Repository, IBuyerRepository
    {
        public IEnumerable<Buyer> GetAll(long companyId, long sobId, DateTime startDate, DateTime endDate)
        {
            IEnumerable<Buyer> list = this.Context.Buyers.Where(x => x.CompanyId == companyId && x.SOBId == sobId && (x.StartDate <= startDate || x.StartDate == null) && (x.EndDate >= endDate || x.EndDate == null));
            return list;
        }

        public IEnumerable<Buyer> GetAll(long companyId, long sobId)
        {
            IEnumerable<Buyer> list = this.Context.Buyers.Where(x => x.CompanyId == companyId && x.SOBId == sobId);
            return list;
        }

        public Buyer GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            return this.Context.Buyers.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
        }

        public IEnumerable<Buyer> GetAll(long companyId)
        {
            return this.Context.Buyers.Where(x => x.CompanyId == companyId);
        }

        public string Insert(Buyer entity)
        {
            this.Context.Buyers.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Buyer entity)
        {
            var originalEntity = this.Context.Buyers.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Buyers.Remove(this.GetSingle(id, companyId));
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
