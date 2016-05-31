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

        public LotNumber GetLotBySourceId(long sourceId, long companyId, long sobId)
        {
            LotNumber entity = this.Context.LotNumbers.FirstOrDefault(x => x.CompanyId == companyId && x.SOBId == sobId && x.SourceId == sourceId);
            return entity;
        }

        public SerialNumber GetSingleSerialNum(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            SerialNumber entity = this.Context.SerialNumbers.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId);
            return entity;
        }

        public SerialNumber GetSerialNo(string serial, long lotNoId, long companyId, long sobId)
        {
            return this.Context.SerialNumbers.Where(rec => rec.CompanyId == companyId && rec.LotNoId == lotNoId && rec.SerialNo == serial).
                OrderByDescending(cri => cri.Id).FirstOrDefault();
        }

        public List<SerialNumber> GetSerialsbyLotNo(long lotNoId, long companyId, long sobId)
        {
            return this.Context.SerialNumbers.Where(rec => rec.LotNoId == lotNoId && rec.CompanyId == companyId).ToList();
        }

        public IEnumerable<LotNumber> GetAll(long companyId)
        {
            IEnumerable<LotNumber> entityList = this.Context.LotNumbers.Where(x => x.CompanyId == companyId);
            return entityList;
        }

        public IEnumerable<LotNumber> GetAllbyLotNo(long companyId, long sobId, string lotNo, long itemId)
        {
            IEnumerable<LotNumber> entityList = this.Context.LotNumbers.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.LotNo == lotNo && x.ItemId == itemId);
            return entityList;
        }

        public IEnumerable<LotNumber> CheckLotNumAvailability(long companyId, string lotNum, long itemId, long sobId)
        {
            return this.Context.LotNumbers.Where(rec => rec.CompanyId == companyId && rec.LotNo == lotNum && rec.ItemId == itemId && rec.SOBId == sobId);
        }

        public bool CheckSerialNumAvailability(long companyId, long lotNoId, string serialNum)
        {
            IEnumerable<SerialNumber> serials = this.Context.SerialNumbers.Where(rec => rec.CompanyId == companyId && rec.LotNoId == lotNoId && rec.SerialNo == serialNum);
            if (serials.Count() % 2 == 0)
                return false;
            else
                return true;
        }

        public IEnumerable<LotNumber> GetAvailableLots(long companyId, long sobId, long itemId)
        {
            IEnumerable<LotNumber> mainQuery = this.Context.LotNumbers.Where(rec => rec.CompanyId == companyId &&
                rec.SOBId == sobId && rec.ItemId == itemId).ToList();
            List<LotNumber> availableLots = new List<LotNumber>();

            IEnumerable<LotNumber> received = mainQuery.Where(rec => rec.SourceType == "Move Order" || rec.SourceType == "Receiving");
            IEnumerable<LotNumber> shipped = mainQuery.Where(rec => rec.SourceType == "Shipment");

            availableLots.AddRange(mainQuery.Where(x => x.LotNo == received.First(y => y.LotNo == x.LotNo).LotNo &&
                !shipped.Any(cri => cri.LotNo == x.LotNo)).ToList());

            foreach (var lot in mainQuery)
            {
                List<SerialNumber> serialExist = this.GetSerialsbyLotNo(lot.Id, companyId, sobId).ToList();
                if (serialExist != null && serialExist.Count > 0)
                {
                    var availableSerials = from a in serialExist
                                           group a by a.SerialNo into g
                                           where g.Count() % 2 != 0
                                           select new
                                           {
                                           };
                    if (availableSerials.Count() > 0)
                    {
                        if (!availableLots.Any(rec => rec.Id == lot.Id))
                            availableLots.Add(lot);
                    }
                }
            }

            return availableLots;
        }

        public IEnumerable<SerialNumber> GetAvailableSerials(LotNumber entity, long companyId, long sobId)
        {
            List<SerialNumber> serialExist = this.GetSerialsbyLotNo(entity.Id, companyId, sobId).ToList();
            if (serialExist != null && serialExist.Count > 0)
            {
                var availableSerials = (from a in serialExist
                                       group a by a.SerialNo into g
                                       where g.Count() % 2 != 0
                                       select new SerialNumber
                                       {
                                           CompanyId = g.FirstOrDefault().CompanyId,
                                           CreateBy = g.FirstOrDefault().CreateBy,
                                           CreateDate = g.FirstOrDefault().CreateDate,
                                           Id = g.FirstOrDefault().Id,
                                           LotNo = g.FirstOrDefault().LotNo,
                                           LotNoId = g.FirstOrDefault().LotNoId,
                                           SerialNo = g.FirstOrDefault().SerialNo,
                                           UpdateBy = g.FirstOrDefault().UpdateBy,
                                           UpdateDate = g.FirstOrDefault().UpdateDate
                                       }).ToList();
                
                return availableSerials;
            }
            return new List<SerialNumber>();
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
