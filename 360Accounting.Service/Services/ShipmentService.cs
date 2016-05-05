using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Service
{
    public class ShipmentService : IShipmentService
    {
        private IShipmentRepository repository;

        public ShipmentService(IShipmentRepository repo)
        {
            this.repository = repo;
        }

        public IEnumerable<Shipment> GetAll(long companyId, long sobId)
        {
            return this.repository.GetAll(companyId, sobId);
        }

        public IEnumerable<Shipment> GetAllByOrderId(long companyId, long sobId, long orderId)
        {
            return this.repository.GetAllByOrderId(companyId, sobId, orderId);
        }

        public IEnumerable<Shipment> GetAllWarehouseId(long companyId, long sobId, long warehouseId)
        {
            return this.repository.GetAllByOrderId(companyId, sobId, warehouseId);
        }

        public IEnumerable<Shipment> GetAllByLocatorId(long companyId, long sobId, long locatorId)
        {
            return this.repository.GetAllByOrderId(companyId, sobId, locatorId);
        }

        public IEnumerable<Shipment> GetAllByLineId(long companyId, long sobId, long lineId)
        {
            return this.repository.GetAllByOrderId(companyId, sobId, lineId);
        }

        public Shipment GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<Shipment> GetAll(long companyId)
        {
            return this.repository.GetAll(companyId);
        }

        public string Insert(Shipment entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Shipment entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public string BatchInsert(List<Shipment> entities)
        {
            return this.repository.BatchInsert(entities);
        }

        public string BatchUpdate(List<Shipment> entities)
        {
            return this.repository.BatchUpdate(entities);
        }

        public void BatchDelete(List<Shipment> entities)
        {
            this.repository.BatchDelete(entities);
        }

        public void DeleteByOrderId(long companyId, long sobId, long orderId)
        {
            this.repository.DeleteByOrderId(companyId, sobId, orderId);
        }

        public int Count(long companyId)
        {
            return this.repository.Count(companyId);
        }
    }
}
