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

        public OrderDetail GetSingleOrderDetail(long id)
        {
            return this.Context.OrderDetails.FirstOrDefault(rec => rec.Id == id);
        }

        public IEnumerable<OrderView> GetAllOrders(long companyId, long sobId)
        {
            var query = from a in this.Context.Orders
                        join b in this.Context.OrderTypes on a.OrderTypeId equals b.Id
                        join c in this.Context.Customers on a.CustomerId equals c.Id
                        join d in this.Context.CustomerSites on a.CustomerSiteId equals d.Id
                        where a.CompanyId == companyId && a.SOBId == sobId
                        select new OrderView
                        {
                            CompanyId = a.CompanyId,
                            OrderTypeName = b.OrderTypeName,
                            CustomerSiteName = d.SiteName,
                            CustomerName = c.CustomerName,
                            CreateBy = a.CreateBy,
                            CreateDate = a.CreateDate,
                            CustomerId = a.CustomerId,
                            CustomerSiteId = a.CustomerSiteId,
                            Id = a.Id,
                            OrderDate = a.OrderDate,
                            OrderNo = a.OrderNo,
                            OrderTypeId = a.OrderTypeId,
                            Remarks = a.Remarks,
                            SOBId = a.SOBId,
                            Status = a.Status,
                            UpdateBy = a.UpdateBy,
                            UpdateDate = a.UpdateDate
                        };

            return query.ToList();
        }
        
        public IEnumerable<Order> GetAll(long sobId)
        {
            IEnumerable<Order> list = this.Context.Orders.Where(x => x.SOBId == sobId);
            return list;
        }

        public IEnumerable<Order> GetAll(long companyId, long sobId, long customerId, long customerSiteId)
        {
            IEnumerable<Order> list = this.Context.Orders.Where(x => x.CompanyId == companyId && x.SOBId == sobId && x.CustomerId == customerId && x.CustomerSiteId == customerSiteId && x.Status != "Shipped");
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
