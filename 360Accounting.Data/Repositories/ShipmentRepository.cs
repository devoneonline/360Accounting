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
    public class ShipmentRepository : Repository, IShipmentRepository
    {
        public IEnumerable<Shipment> GetAll(long companyId, long sobId)
        {
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId);
            return list;
        }

        public IEnumerable<ShipmentView> GetAllShipments(long companyId, long sobId)
        {
            var query = from a in this.Context.Shipments
                        group a by a.DeliveryDate into g
                        join b in this.Context.Orders on g.FirstOrDefault().OrderId equals b.Id
                        join c in this.Context.Customers on b.CustomerId equals c.Id
                        join d in this.Context.CustomerSites on b.CustomerSiteId equals d.Id
                        where g.FirstOrDefault().CompanyId == companyId && g.FirstOrDefault().SOBId == sobId
                        select new ShipmentView
                        {
                            CompanyId = g.FirstOrDefault().CompanyId,
                            CustomerSiteName = d.SiteName,
                            CustomerName = c.CustomerName,
                            OrderNo = b.OrderNo,
                            CreateBy = g.FirstOrDefault().CreateBy,
                            CreateDate = g.FirstOrDefault().CreateDate,
                            DeliveryDate = g.Key,
                            Id = g.FirstOrDefault().Id,
                            OrderId = g.FirstOrDefault().OrderId,
                            Quantity = g.Sum(x => x.Quantity),
                            SOBId = g.FirstOrDefault().SOBId,
                            UpdateBy = g.FirstOrDefault().UpdateBy,
                            UpdateDate = g.FirstOrDefault().UpdateDate
                        };

            return query.OrderByDescending(rec => rec.Id).ToList();
        }

        public IEnumerable<Shipment> GetAllByOrderId(long companyId, long sobId, long orderId, DateTime date)
        {
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.OrderId == orderId && x.DeliveryDate == date);
            return list;
        }

        public IEnumerable<Shipment> GetAllWarehouseId(long companyId, long sobId, long warehouseId)
        {
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.WarehouseId == warehouseId);
            return list;
        }

        public IEnumerable<Shipment> GetAllByLocatorId(long companyId, long sobId, long locatorId)
        {
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.LocatorId == locatorId);
            return list;
        }

        public IEnumerable<Shipment> GetAllByLineId(long companyId, long sobId, long lineId)
        {
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.LineId == lineId);
            return list;
        }

        public Shipment GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            Shipment shipment = this.Context.Shipments.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId);
            return shipment;
        }

        public IEnumerable<Shipment> GetAll(long companyId)
        {
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId);
            return list;
        }

        public string Insert(Shipment entity)
        {
            this.Context.Shipments.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Shipment entity)
        {
            var originalEntity = this.Context.Shipments.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.Shipments.Remove(this.GetSingle(id, companyId));
            this.Commit();
        }

        public string BatchInsert(List<Shipment> entities)
        {
            foreach (var item in entities)
            {
                this.Context.Shipments.Add(item);
            }
            this.Commit();
            return this.Context.Shipments.Count().ToString();
        }

        public string BatchUpdate(List<Shipment> entities)
        {
            foreach (var item in entities)
            {
                var originalEntity = this.Context.Shipments.Find(item.Id);
                this.Context.Entry(originalEntity).CurrentValues.SetValues(item);
                this.Context.Entry(originalEntity).State = EntityState.Modified;
            }
            
            this.Commit();
            return this.Context.Shipments.Count().ToString();
        }

        public void BatchDelete(List<Shipment> entities)
        {
            foreach (var item in entities)
            {
                this.Context.Shipments.Remove(this.Context.Shipments.FirstOrDefault(x => x.Id == item.Id));
            }
            this.Commit();
        }

        public void DeleteByOrderId(long companyId, long sobId, long orderId, DateTime date)
        {
            List<Shipment> shipments = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.OrderId == orderId && x.DeliveryDate == date).ToList();
            if (shipments != null && shipments.Count() > 0)
            {
                foreach (var item in shipments)
                {
                    this.Context.Shipments.Remove(item);
                }
                this.Commit();
            }
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
