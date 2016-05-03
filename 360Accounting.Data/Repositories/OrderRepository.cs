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
    public class OrderRepository : Repository, IOrderRepository
    {
        public IEnumerable<OrderDetail> GetAllOrderDetail(long orderId)
        {
            IEnumerable<OrderDetail> list = this.Context.OrderDetails.Where(x => x.OrderId == orderId);
            return list;
        }

        public IEnumerable<OrderDetail> GetOrderDetailByItem(long itemId)
        {
            IEnumerable<OrderDetail> list = this.Context.OrderDetails.Where(x => x.ItemId == itemId);
            return list;
        }

        public IEnumerable<OrderDetail> GetOrderDetailByWarehouse(long warehouseId)
        {
            IEnumerable<OrderDetail> list = this.Context.OrderDetails.Where(x => x.WarehouseId == warehouseId);
            return list;
        }

        public IEnumerable<OrderDetail> GetOrderDetailByTax(long taxId)
        {
            IEnumerable<OrderDetail> list = this.Context.OrderDetails.Where(x => x.TaxId == taxId);
            return list;
        }

        public string Insert(OrderDetail entity)
        {
            this.Context.OrderDetails.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(OrderDetail entity)
        {
            var originalEntity = this.Context.OrderDetails.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void DeleteOrderDetail(long id)
        {
            this.Context.OrderDetails.Remove(this.Context.OrderDetails.FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<Order> GetAll(long sobId)
        {
            IEnumerable<Order> list = this.Context.Orders.Where(x => x.SOBId == sobId);
            return list;
        }

        public IEnumerable<Order> GetAllOrdersByOrderType(long sobId, long orderTypeId)
        {
            IEnumerable<Order> list = this.Context.Orders.Where(x => x.SOBId == sobId && x.OrderTypeId == orderTypeId);
            return list;
        }

        public Order GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);

            Order order = this.Context.Orders.FirstOrDefault(x => x.Id == longId);
            return order;
        }

        public string Insert(Order entity)
        {
            this.Context.Orders.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(Order entity)
        {
            var originalEntity = this.Context.Orders.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            this.Context.Orders.Remove(this.Context.Orders.FirstOrDefault(x => x.Id == longId && x.CompanyId == companyId));
        }

        public int Count(long companyId)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            return this.Context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrdersByCustomer(long sobId, long customerId)
        {
            IEnumerable<Order> list = this.Context.Orders.Where(x => x.SOBId == sobId && x.CustomerId == customerId);
            return list;
        }

        public IEnumerable<Order> GetAllOrdersByCustomerSite(long sobId, long customerSiteId)
        {
            IEnumerable<Order> list = this.Context.Orders.Where(x => x.SOBId == sobId && x.CustomerSiteId == customerSiteId);
            return list;
        }
    }
}
