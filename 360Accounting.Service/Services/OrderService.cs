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
        private IOrderRepository repository;

        public OrderService(IOrderRepository repo)
        {
            this.repository = repo;
        }

        public Order GetSingle(string id, long companyId)
        {
            return this.repository.GetSingle(id, companyId);
        }

        public IEnumerable<OrderDetail> GetAllOrderDetail(long orderId)
        {
            return this.repository.GetAllOrderDetail(orderId);
        }

        public IEnumerable<OrderDetail> GetOrderDetailByItem(long itemId)
        {
            return this.repository.GetOrderDetailByItem(itemId);
        }

        public IEnumerable<OrderDetail> GetOrderDetailByWarehouse(long warehouseId)
        {
            return this.repository.GetOrderDetailByWarehouse(warehouseId);
        }

        public IEnumerable<OrderDetail> GetOrderDetailByTax(long taxId)
        {
            return this.repository.GetOrderDetailByTax(taxId);
        }

        public OrderDetail GetSingleOrderDetail(long id)
        {
            return this.repository.GetSingleOrderDetail(id);
        }

        public string Insert(OrderDetail entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(OrderDetail entity)
        {
            return this.repository.Update(entity);
        }

        public void DeleteOrderDetail(long id)
        {
            this.repository.DeleteOrderDetail(id);
        }

        public IEnumerable<Order> GetAll(long sobId)
        {
            return this.repository.GetAll(sobId);
        }

        public IEnumerable<Order> GetAll(long companyId, long sobId, long customerId, long customerSiteId)
        {
            return this.repository.GetAll(companyId, sobId, customerId, customerSiteId);
        }

        public IEnumerable<Order> GetAllOrdersByOrderType(long sobId, long orderTypeId)
        {
            return this.repository.GetAllOrdersByOrderType(sobId, orderTypeId);
        }

        public IEnumerable<Order> GetAllOrdersByCustomer(long sobId, long customerId)
        {
            return this.repository.GetAllOrdersByCustomer(sobId, customerId);
        }

        public IEnumerable<Order> GetAllOrdersByCustomerSite(long sobId, long customerSiteId)
        {
            return this.repository.GetAllOrdersByCustomerSite(sobId, customerSiteId);
        }

        public string Insert(Order entity)
        {
            return this.repository.Insert(entity);
        }

        public string Update(Order entity)
        {
            return this.repository.Update(entity);
        }

        public void Delete(string id, long companyId)
        {
            this.repository.Delete(id, companyId);
        }

        public int Count(long companyId)
        {
            throw new NotImplementedException();
        }
    }
}
