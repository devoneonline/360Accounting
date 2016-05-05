using _360Accounting.Core.Entities;
using _360Accounting.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
{
    public interface IShipmentService : IService<Shipment>
    {
        IEnumerable<Shipment> GetAll(long companyId, long sobId);
        IEnumerable<Shipment> GetAllByOrderId(long companyId, long sobId, long orderId);
        IEnumerable<Shipment> GetAllWarehouseId(long companyId, long sobId, long warehouseId);
        IEnumerable<Shipment> GetAllByLocatorId(long companyId, long sobId, long locatorId);
        IEnumerable<Shipment> GetAllByLineId(long companyId, long sobId, long lineId);

        void DeleteByOrderId(long companyId, long sobId, long orderId);
        string BatchUpdate(List<Shipment> entities);
        string BatchInsert(List<Shipment> entities);
        void BatchDelete(List<Shipment> entities);
    }
}
