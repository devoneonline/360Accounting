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
    public class ReceivingRepository : Repository, IReceivingRepository
    {
        public IEnumerable<ReceivingDetailView> GetAllReceivingDetail(long receivingId)
        {
            var query = from a in this.Context.ReceivingDetails
                        join b in this.Context.Items on a.ItemId equals b.Id
                        join c in this.Context.LotNumbers on a.LotNoId equals c.Id into leftLot
                        from left in leftLot.DefaultIfEmpty()
                        where a.ReceiptId == receivingId
                        select new ReceivingDetailView
                        {
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            Id = a.Id,
                            ItemId = a.ItemId,
                            ItemName = b.ItemName,
                            LocatorId = a.LocatorId,
                            LotNo = left.LotNo,
                            LotNoId = a.LotNoId,
                            Quantity = a.Quantity,
                            ReceiptId = a.ReceiptId,
                            SerialNo = a.SerialNo,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate,
                            WarehouseId = a.WarehouseId,
                            PODetailId = a.PODetailId

                        };

            return query;
        }

        public string Insert(ReceivingDetail entity)
        {
            this.Context.ReceivingDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(ReceivingDetail entity)
        {
            var originalEntity = this.Context.ReceivingDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void DeleteReceivingDetail(long id)
        {
            this.Context.ReceivingDetails.Remove(this.Context.ReceivingDetails.FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<Receiving> GetAll(long companyId)
        {
            IEnumerable<Receiving> list = this.Context.Receivings.Where(x => x.CompanyId == companyId);
            return list;
        }

        public IEnumerable<Receiving> GetAllByPOId(long companyId, long sobId, long poId)
        {
            IEnumerable<Receiving> list = this.Context.Receivings.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.POId == poId);
            return list;
        }

        public IEnumerable<ReceivingDetail> GetAllByPODetailId(long poDetailId)
        {
            IEnumerable<ReceivingDetail> list = this.Context.ReceivingDetails.Where(x => x.PODetailId == poDetailId);
            return list;
        }

        public IEnumerable<ReceivingView> GetAll(long companyId, long sobId)
        {
            var query = from a in this.Context.Receivings
                        join b in this.Context.PurchaseOrders on a.POId equals b.Id
                        where a.CompanyId == companyId && a.SOBId == sobId
                        select new ReceivingView
                        {
                            CompanyId = a.CompanyId,
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            Date = a.Date,
                            DCNo = a.DCNo,
                            Id = a.Id,
                            POId = a.POId,
                            PONo = b.PONo,
                            ReceiptNo = a.ReceiptNo,
                            SOBId = a.SOBId,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate
                        };

            return query;
        }

        public Receiving GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            Receiving receiving = this.Context.Receivings.FirstOrDefault(x => x.Id == longId);
            return receiving;
        }

        public ReceivingDetail GetSingleReceivingDetail(long id)
        {
            return this.Context.ReceivingDetails.FirstOrDefault(rec => rec.Id == id);
        }

        public string Insert(Receiving entity)
        {
            this.Context.Receivings.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Receiving entity)
        {
            var originalEntity = this.Context.Receivings.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            this.Context.Receivings.Remove(this.Context.Receivings.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId));
            this.Commit();
        }

        public int Count(long companyId)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }
    }
}