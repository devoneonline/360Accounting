using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        //OrderDetail GetSingleOrderDetail(string id, long companyId);
        IEnumerable<OrderDetail> GetAllOrderDetail(long orderId);
        IEnumerable<OrderDetail> GetOrderDetailByItem(long itemId);
        IEnumerable<OrderDetail> GetOrderDetailByWarehouse(long warehouseId);
        IEnumerable<OrderDetail> GetOrderDetailByTax(long taxId);
        string Insert(OrderDetail entity);
        string Update(OrderDetail entity);
        void DeleteOrderDetail(long id);

        IEnumerable<Order> GetAll(long sobId);
        IEnumerable<Order> GetAllOrdersByOrderType(long sobId, long orderTypeId);
        IEnumerable<Order> GetAllOrdersByCustomer(long sobId, long customerId);
        IEnumerable<Order> GetAllOrdersByCustomerSite(long sobId, long customerSiteId);

        IEnumerable<Order> GetAll(long companyId, long sobId, long customerId, long customerSiteId);
        IEnumerable<OrderView> GetAllOrders(long companyId, long sobId);

        OrderDetail GetSingleOrderDetail(long id);
    }
}
