using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IShipmentRepository : IRepository<Shipment>
    {
        IEnumerable<Shipment> GetAll(long companyId, long sobId);
        IEnumerable<Shipment> GetDelivery(long companyId, long sobId, string deliveryNo, DateTime date);
        IEnumerable<Shipment> GetAllWarehouseId(long companyId, long sobId, long warehouseId);
        IEnumerable<Shipment> GetAllByLocatorId(long companyId, long sobId, long locatorId);
        IEnumerable<Shipment> GetAllByLineId(long companyId, long sobId, long lineId);

        void DeleteDelivery(long companyId, long sobId, string deliveryNo, DateTime date);
        string BatchInsert(List<Shipment> entities);
        string BatchUpdate(List<Shipment> entities);
        void BatchDelete(List<Shipment> entities);

        IEnumerable<ShipmentView> GetAllShipments(long companyId, long sobId);
    }
}
