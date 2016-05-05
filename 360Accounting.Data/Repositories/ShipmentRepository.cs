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
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId)
                .GroupBy(rec => rec.OrderId).Select(x => x.FirstOrDefault());
            return list;
        }

        public IEnumerable<Shipment> GetAllByOrderId(long companyId, long sobId, long orderId)
        {
            IEnumerable<Shipment> list = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.OrderId == orderId);
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

        public void DeleteByOrderId(long companyId, long sobId, long orderId)
        {
            List<Shipment> shipments = this.Context.Shipments.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.OrderId == orderId).ToList();
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
