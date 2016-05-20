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
    public class RFQRepository : Repository, IRFQRepository
    {
        public IEnumerable<RFQDetail> GetAllRFQDetail(long rfqId)
        {
            IEnumerable<RFQDetail> list = this.Context.RFQDetails.Where(x => x.RFQId == rfqId);
            return list;
        }

        public string InsertRFQDetail(RFQDetail entity)
        {
            this.Context.RFQDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string UpdateRFQDetail(RFQDetail entity)
        {
            var originalEntity = this.Context.RFQDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void DeleteRFQDetail(long id)
        {
            this.Context.RFQDetails.Remove(this.Context.RFQDetails.FirstOrDefault(x => x.Id == id));
            this.Commit();
        }

        public RFQ GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            return this.Context.RFQs.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
        }

        public IEnumerable<RFQ> GetAll(long companyId)
        {
            return this.Context.RFQs.Where(x => x.CompanyId == companyId);
        }

        public string Insert(RFQ entity)
        {
            this.Context.RFQs.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(RFQ entity)
        {
            var originalEntity = this.Context.RFQs.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.RFQs.Remove(this.GetSingle(id, companyId));
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
