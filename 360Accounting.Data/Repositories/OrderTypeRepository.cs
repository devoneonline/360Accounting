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
    public class OrderTypeRepository : Repository, IOrderTypeRepository
    {
        public IEnumerable<OrderType> GetOrderTypes(long companyId, long sobId)
        {
            IEnumerable<OrderType> list = this.Context.OrderTypes.Where(x => x.CompanyId == companyId && x.SOBId == sobId);
            return list;
        }

        public IEnumerable<OrderType> GetOrderTypes(long companyId, long sobId, DateTime date)
        {
            IEnumerable<OrderType> list = this.Context.OrderTypes.Where(x => x.CompanyId == companyId && x.SOBId == sobId && (x.DateTo >= date || x.DateTo == null));
            return list;
        }

        public OrderType GetSingle(string id, long companyId)
        {
            long longId = Convert.ToInt64(id);
            OrderType orderType = this.Context.OrderTypes.FirstOrDefault(x => x.CompanyId == companyId && x.Id == longId);
            return orderType;
        }

        public IEnumerable<OrderType> GetAll(long companyId)
        {
            IEnumerable<OrderType> list = this.Context.OrderTypes.Where(x => x.CompanyId == companyId);
            return list;
        }

        public string Insert(OrderType entity)
        {
            this.Context.OrderTypes.Add(entity);
            this.Commit();
            return entity.Id.ToString();
        }

        public string Update(OrderType entity)
        {
            var originalEntity = this.Context.OrderTypes.Find(entity.Id);
            this.Context.Entry(originalEntity).CurrentValues.SetValues(entity);
            this.Context.Entry(originalEntity).State = EntityState.Modified;
            this.Commit();
            return entity.Id.ToString();
        }

        public void Delete(string id, long companyId)
        {
            this.Context.OrderTypes.Remove(this.GetSingle(id, companyId));
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
    }
}
