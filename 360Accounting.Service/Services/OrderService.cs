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
    public class OrderService : IOrderService
    {
        public IEnumerable<OrderDetail> GetAllOrderDetail(long orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetOrderDetailByItem(long itemId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetOrderDetailByWarehouse(long warehouseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetOrderDetailByTax(long taxId)
        {
            throw new NotImplementedException();
        }

        public string Insert(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public string Update(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderDetail(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll(long sobId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrdersByOrderType(long sobId, long orderTypeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrdersByCustomer(long sobId, long customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrdersByCustomerSite(long sobId, long customerSiteId)
        {
            throw new NotImplementedException();
        }
    }
}
