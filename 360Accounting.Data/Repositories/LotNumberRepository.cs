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
    public class LotNumberRepository : Repository, ILotNumberRepository
    {
        public LotNumber GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            LotNumber entity = this.Context.LotNumbers.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId);
            return entity;
        }

        public IEnumerable<LotNumber> GetAll(long companyId)
        {
            IEnumerable<LotNumber> entityList = this.Context.LotNumbers.Where(x => x.CompanyId == companyId);
            return entityList;
        }

        public bool CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId)
        {
            if (this.Context.LotNumbers.Where(x => x.CompanyId == companyId && x.LotNo == lotNum && x.SOBId == sobId && x.ItemId == itemId).Count() > 0)
                return false;
            else
                return true;
        }

        public bool CheckSerialNumAvailability(long companyId, string lotNum, string serialNum)
        {
            if (this.Context.SerialNumbers.Where(x => x.LotNo == lotNum && x.CompanyId == companyId && x.SerialNo == serialNum).Count() > 0)
                return false;
            else
                return true;
        }

        public string Insert(LotNumber entity)
        {
            this.Context.LotNumbers.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(LotNumber entity)
        {
            LotNumber originalEntity = this.Context.LotNumbers.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.LotNumbers.Remove(this.GetSingle(id, companyId));
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
