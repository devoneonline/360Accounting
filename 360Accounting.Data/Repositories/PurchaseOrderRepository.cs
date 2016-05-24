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
    public class PurchaseOrderRepository : Repository, IPurchaseOrderRepository
    {
        public IEnumerable<PurchaseOrderDetail> GetAllPODetail(long poId)
        {
            IEnumerable<PurchaseOrderDetail> list = this.Context.PurchaseOrderDetails.Where(x => x.POId == poId);
            return list;
        }

        public string Insert(PurchaseOrderDetail entity)
        {
            this.Context.PurchaseOrderDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PurchaseOrderDetail entity)
        {
            var originalEntity = this.Context.PurchaseOrderDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void DeletePODetail(long id)
        {
            this.Context.PurchaseOrderDetails.Remove(this.Context.PurchaseOrderDetails.FirstOrDefault(x => x.Id == id));
        }

        public PurchaseOrderDetail GetSinglePODetail(long id)
        {
            return this.Context.PurchaseOrderDetails.FirstOrDefault(rec => rec.Id == id);
        }

        public IEnumerable<PurchaseOrder> GetAll(long companyId)
        {
            IEnumerable<PurchaseOrder> list = this.Context.PurchaseOrders.Where(x => x.CompanyId == companyId);
            return list;
        }

        public IEnumerable<PurchaseOrder> GetAll(long companyId, long sobId)
        {
            IEnumerable<PurchaseOrder> list = this.Context.PurchaseOrders.Where(x => x.CompanyId == companyId && x.SOBId == sobId);
            return list;
        }

        public IEnumerable<PurchaseOrderView> GetAllPO(long companyId, long sobId)
        {
            var query = from a in this.Context.PurchaseOrders
                        join b in this.Context.Vendors on a.VendorId equals b.Id
                        join c in this.Context.VendorSites on a.VendorSiteId equals c.Id
                        join d in this.Context.Buyers on a.BuyerId equals d.Id
                        where a.CompanyId == companyId && a.SOBId == sobId
                        select new PurchaseOrderView
                        {
                            BuyerId = a.BuyerId,
                            BuyerName = d.Name,
                            CompanyId = a.CompanyId,
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            Description = a.Description,
                            Id = a.Id,
                            PODate = a.PODate,
                            PONo = a.PONo,
                            SOBId = a.SOBId,
                            Status = a.Status,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate,
                            VendorId = a.VendorId,
                            VendorName = b.Name,
                            VendorSiteId = a.VendorSiteId,
                            VendorSiteName = c.Name
                        };

            return query;
        }

        public PurchaseOrder GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);

            PurchaseOrder po = this.Context.PurchaseOrders.FirstOrDefault(x => x.Id == longId);
            return po;
        }

        public string Insert(PurchaseOrder entity)
        {
            this.Context.PurchaseOrders.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(PurchaseOrder entity)
        {
            var originalEntity = this.Context.PurchaseOrders.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            this.Context.PurchaseOrders.Remove(this.Context.PurchaseOrders.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId));
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