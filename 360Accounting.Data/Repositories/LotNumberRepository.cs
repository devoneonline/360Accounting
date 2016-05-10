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

        public SerialNumber GetSingleSerialNum(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            SerialNumber entity = this.Context.SerialNumbers.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId);
            return entity;
        }

        public IEnumerable<LotNumber> GetAll(long companyId)
        {
            IEnumerable<LotNumber> entityList = this.Context.LotNumbers.Where(x => x.CompanyId == companyId);
            return entityList;
        }

        public IEnumerable<LotNumber> CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId)
        {
            return this.Context.LotNumbers.Where(rec => rec.CompanyId == companyId && rec.LotNo == lotNum && rec.ItemId == itemId && rec.SOBId == sobId);
        }

        public IEnumerable<SerialNumber> CheckSerialNumAvailability(long companyId, string lotNum, string serialNum)
        {
            return this.Context.SerialNumbers.Where(rec => rec.CompanyId == companyId && rec.LotNo == lotNum && rec.SerialNo == serialNum);
        }

        public IEnumerable<LotNumber> GetAvailableLots(long companyId, long sobId, long itemId)
        {
            IEnumerable<LotNumber> mainQuery = this.Context.LotNumbers.Where(rec => rec.CompanyId == companyId &&
                rec.SOBId == sobId && rec.ItemId == itemId);
            IEnumerable<LotNumber> received = mainQuery.Where(rec => rec.SourceType == "Move Order" || rec.SourceType == "Receiving");
            IEnumerable<LotNumber> shipped = mainQuery.Where(rec => rec.SourceType == "Shipment");

            IEnumerable<LotNumber> available = mainQuery.Where(x => x.LotNo == received.First(y => y.LotNo == x.LotNo).LotNo && 
                !shipped.Any(cri => cri.LotNo == x.LotNo)).ToList();

            return available;
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

        public string InsertSerialNum(SerialNumber entity)
        {
            this.Context.SerialNumbers.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string UpdateSerialNum(SerialNumber entity)
        {
            SerialNumber originalEntity = this.Context.SerialNumbers.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void DeleteSerialNum(string id, long companyId)
        {
            this.Context.SerialNumbers.Remove(this.GetSingleSerialNum(id, companyId));
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
